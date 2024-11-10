using Newtonsoft.Json;
using System.Diagnostics;
using System.Linq.Expressions;

namespace acProj
{
    public partial class Wnd : Form
    {

        List<string> m_repos = [];
        List<string> m_filteredRepos = [];
        int m_idx = 0;
        string m_editor = "";
        string m_repoDir = "";

        public Wnd()
        {
            InitializeComponent();
            this.Icon = new Icon("Resources/ico.ico");

            using var sr = new StreamReader("config.json");
            var cfg = JsonConvert.DeserializeObject<ConfigDTO>(sr.ReadToEnd());
            m_editor = cfg.codeEditor;
            m_repoDir = cfg.repoDir;
            var repos = Directory.GetDirectories(cfg.repoDir);
            foreach (var reposItem in repos)
            {
                m_repos.Add(reposItem);
                m_filteredRepos.Add(reposItem);
            }

            RenderDirs();
        }

        private void FilterDirs()
        {
            m_filteredRepos.Clear();
            foreach (var reposItem in m_repos)
            {
                if (reposItem.ToLower().Contains(label1.Text))
                    m_filteredRepos.Add(reposItem);
            }

            if (m_idx > m_filteredRepos.Count - 1)
            {
                m_idx = m_filteredRepos.Count - 1;
            }
        }

        private void RenderDirs()
        {
            panel1.Controls.Clear();
            for (int i=m_filteredRepos.Count - 1; i >= 0; i--)
            {
                var color = i == m_idx
                    ? Color.FromArgb(37, 36, 52)
                    : Color.FromArgb(30, 30, 46);
                var newLabel = new System.Windows.Forms.Label()
                {
                    Text = new DirectoryInfo(m_filteredRepos[i]).Name,
                    BackColor = color,
                    Dock = DockStyle.Top,
                    Font = new Font("Segoe UI", 18, FontStyle.Regular),
                    ForeColor = Color.FromArgb(191, 198, 212),
                    Size = new System.Drawing.Size(319, 35)
                };
                panel1.Controls.Add(newLabel);
                //Controls.Add(newLabel);
            }
        }

        private void Wnd_Deactivate(object sender, EventArgs e) 
        {
            this.Close();
        }

        private void Wnd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();

            if (e.KeyCode == Keys.Back && label1.Text.Length > 0)
                label1.Text = label1.Text.Substring(0, label1.Text.Length - 1);

            if (e.Modifiers == Keys.Shift && e.KeyCode == Keys.Enter)
            {
                var newPath = Path.Combine(m_repoDir, label1.Text);
                if (!Directory.Exists(newPath))
                {
                    InitProject(newPath);
                    OpenProject(newPath);
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                var selectedPath = m_filteredRepos[m_idx];
                OpenProject(selectedPath);
            }

            if (e.KeyCode == Keys.Up && m_idx > 0)
            {
                m_idx--;
                RenderDirs();
            }

            if (e.KeyCode == Keys.Down && m_idx <  m_filteredRepos.Count - 1)
            {
                m_idx++;
                RenderDirs();
            }

            if ("abcdefghijklmnopqrstuvwxyz".Contains(e.KeyCode.ToString().ToLower()))
            {
                label1.Text += e.KeyCode.ToString().ToLower();
            }
            if (e.Modifiers == Keys.Shift && e.KeyCode == Keys.OemMinus)
                label1.Text += "_";
            else if (e.KeyCode == Keys.OemMinus)
                label1.Text += "-";
        }

        private void label1_TextChanged(object sender, EventArgs e)
        {
            FilterDirs();
            RenderDirs();
        }

        private string ArgBuilder(string path)
        {
            var argStr = "";
            if (File.Exists(path + "\\.acproj"))
            {
                try
                {
                    argStr += " -M";
                    using var sr = new StreamReader(path + "\\.acproj");
                    var projFile = JsonConvert.DeserializeObject<ProjDTO>(sr.ReadToEnd());
                    for (int i = 0; i < projFile?.tabs.Count; i++)
                    {
                        var tab = projFile.tabs[i];
                        if (m_editor != "nvim" && tab.title.ToLower().Contains("nvim"))
                            continue;
                        argStr += argStr == " -M" ? " " :  " ; nt";
                        var dir = path;
                        for (int j = 0; j < tab.panes.Count; j++) 
                        {
                            argStr += j == 0 ? "" : " ; split-pane";
                            var pane = tab.panes[j];
                            var subDir = dir;
                            if (pane.subDir != "")
                                subDir = Path.Combine(dir, pane.subDir);
                            argStr += " -d " + subDir;
                            argStr += " -p \"Windows PowerShell\"";
                            var title = "";
                            if (i < 9) title = "0" + (i+1) + ": " + tab.title;
                            else title = (i+1) + ": " + tab.title;
                            argStr += " --title \"" + title + "\"";
                        }
                    }

                    argStr += " ; ft --target 0";
                }
                catch (Exception)
                {
                    argStr = "";
                    argStr += " -d " + path;
                    argStr += " -p \"Windows PowerShell\"";
                    argStr += " --title \"01: PS\"";
                }
            }
            else
            {
                argStr += " -d " + path;
                argStr += " -p \"Windows PowerShell\"";
                argStr += " --title \"01: PS\"";
            }
            return argStr;
        }

        private void OpenProject(string path)
        {
            var p = new Process();
            p.StartInfo.FileName = "wt.exe";
            p.StartInfo.Arguments = ArgBuilder(path);
            p.Start();

            if (m_editor != "nvim")
            {
                var p2 = new Process();
                p2.StartInfo.FileName = m_editor;
                p2.StartInfo.Arguments = " \"" + path + "\"";
                p2.StartInfo.UseShellExecute = false;
                p2.StartInfo.CreateNoWindow = true;
                p2.Start();
            }
        }

        private void InitProject(string path)
        {
            Directory.CreateDirectory(path);
            File.Copy("default.acproj", path + "\\.acproj");
            
        }
    }
}
