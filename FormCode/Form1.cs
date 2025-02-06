using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Syn.WordNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckCleanCode
{
    public partial class Form1 : Form
    {
        String code;
        WordNetEngine wordNet = new WordNetEngine();

        List<string> variabel_list = new List<string>();
        List<string> class_list = new List<string>();
        List<string> method_list = new List<string>();

        List<string> variabel_in_for = new List<string>();

        List<string> E_classname = new List<string>(); //
        List<string> E_methodname = new List<string>();//
        List<string> E_variabelname = new List<string>();//
        //List<string> E_camelcasename = new List<string>();
        List<string> E_paramMethod = new List<string>();//
        List<string> E_lenMethod = new List<string>();//
        List<string> E_ifline = new List<string>();//
        List<string> E_forline = new List<string>();//
        List<string> E_forechlin = new List<string>();//
        List<string> E_whileline = new List<string>();//

        List<string> E_gotoMethod = new List<string>();//
        List<string> E_trymethod = new List<string>();//
        List<string> E_ifinif = new List<string>();
        List<string> E_forinfor = new List<string>();
        List<string> E_whileinwhile = new List<string>();
        List<string> E_forchinforch = new List<string>();
        List<string> E_methodSingel = new List<string>();//
        List<string> E_methodNotUse = new List<string>();//
        List<string> E_methodMoreUse = new List<string>();//
        List<string> E_variableNotUse = new List<string>();

        List<string> E_forbodylin = new List<string>();//
        List<string> E_forechbodyline = new List<string>();//
        List<string> E_ifbodyline = new List<string>();//
        List<string> E_whilebodyline = new List<string>();//
        List<string> E_elsebodyline = new List<string>();//


        public Form1()
        {
            InitializeComponent();


            this.dataGridView1.Rows.Add("1", "The name of Variable should be Noun","page 17");
            this.dataGridView1.Rows.Add("2", "The name of Class should be Noun", "page 25");
            this.dataGridView1.Rows.Add("3", "The name of Method should be Verb", "page 25");
            this.dataGridView1.Rows.Add("4", "The Condition of if-statement should be one line", "page 35");
            this.dataGridView1.Rows.Add("5", "The Condition of for-statement should be one line", "page 35");
            this.dataGridView1.Rows.Add("6", "The Condition of forech-statement should be one line", "page 35");
            this.dataGridView1.Rows.Add("7", "The Condition of while-statement should be one line", "page 35");

            this.dataGridView1.Rows.Add("8", "The Body of if-statement should be one line", "page 35");
            this.dataGridView1.Rows.Add("9", "The Body of else-statement should be one line", "page 35");
            this.dataGridView1.Rows.Add("10", "The Body of for-statement should be one line", "page 35");
            this.dataGridView1.Rows.Add("11", "The Body of forech-statement should be one line", "page 35");
            this.dataGridView1.Rows.Add("12", "The Body of while-statement should be one line", "page 35");

            this.dataGridView1.Rows.Add("13", "The Parameter of method should be not more than 4", "page 40");
            this.dataGridView1.Rows.Add("14", "The line of method should be not more than 24", "page 34");

            this.dataGridView1.Rows.Add("15", "The method should be not have try-statment", "page 46");
            this.dataGridView1.Rows.Add("16", "The method should be not have Goto", "page 49");

            this.dataGridView1.Rows.Add("17", "The method should be single responsible", "page 35");
            this.dataGridView1.Rows.Add("18", "The method should be not dead code(not use)", "page 288");

            this.dataGridView1.Rows.Add("19", "Call the method should be less than 3", "page 288");
            this.dataGridView1.Rows.Add("20", "Level of if-in-if should be less than 3", "page 35");
            this.dataGridView1.Rows.Add("21", "Level of for-in-for should be less than 3", "page 35");
            this.dataGridView1.Rows.Add("22", "Level of forech-in-forech should be less than 3", "page 35");
            this.dataGridView1.Rows.Add("23", "Level of while-in-while should be less than 3", "page 35");



            var directory = Directory.GetCurrentDirectory();

            wordNet.AddDataSource(new StreamReader(Path.Combine(directory, "data.adj")), PartOfSpeech.Adjective);
            wordNet.AddDataSource(new StreamReader(Path.Combine(directory, "data.adv")), PartOfSpeech.Adverb);
            wordNet.AddDataSource(new StreamReader(Path.Combine(directory, "data.noun")), PartOfSpeech.Noun);
            wordNet.AddDataSource(new StreamReader(Path.Combine(directory, "data.verb")), PartOfSpeech.Verb);

            wordNet.AddIndexSource(new StreamReader(Path.Combine(directory, "index.adj")), PartOfSpeech.Adjective);
            wordNet.AddIndexSource(new StreamReader(Path.Combine(directory, "index.adv")), PartOfSpeech.Adverb);
            wordNet.AddIndexSource(new StreamReader(Path.Combine(directory, "index.noun")), PartOfSpeech.Noun);
            wordNet.AddIndexSource(new StreamReader(Path.Combine(directory, "index.verb")), PartOfSpeech.Verb);
            wordNet.Load();

            
        }
        string alfa = "";

        private void Enterbtn_Click(object sender, EventArgs e)
        {
            int size = -1;
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                try
                {

                    string text = File.ReadAllText(file);
                    size = text.Length;
                    // MessageBox.Show(file);
                    TxtAddress.Text = file;
                    code = text;

                }
                catch (IOException)
                {

                }

            }
        }

        private void cleanbtn_Click(object sender, EventArgs e)
        {
            this.timer1.Start();


            var tree = CSharpSyntaxTree.ParseText(code);
            var root = tree.GetRoot();
            var classDeclarations = root.DescendantNodes().OfType<ClassDeclarationSyntax>();
            var methodDeclarations = root.DescendantNodes().OfType<MethodDeclarationSyntax>();
            var variableDeclarator = root.DescendantNodes().OfType<VariableDeclaratorSyntax>();
            var classDeclarations1 = root.DescendantNodes().OfType<ClassDeclarationSyntax>();
            var methodDeclarations1 = root.DescendantNodes().OfType<MethodDeclarationSyntax>();
            var variableDeclarator1 = root.DescendantNodes().OfType<VariableDeclaratorSyntax>();
            var ifdeclarator = root.DescendantNodes().OfType<IfStatementSyntax>();
            var fordeclaration = root.DescendantNodes().OfType<ForStatementSyntax>();
            var forechdeclaration = root.DescendantNodes().OfType<ForEachStatementSyntax>();
            var whiledeclaration = root.DescendantNodes().OfType<WhileStatementSyntax>();






            foreach ( var iff in ifdeclarator)
            {
                var ifdeclarator1 = iff.DescendantNodes().OfType<IfStatementSyntax>();

                foreach (var iff1 in ifdeclarator1)
                {
                    bool alfa = false;

                    var ifdeclarator2 = iff1.DescendantNodes().OfType<IfStatementSyntax>();

                    foreach(var iff2 in ifdeclarator2)
                    {
                        alfa = true;
                    }
                    if (alfa)
                    {
                        E_ifinif.Add("moreIF");
                       // MessageBox.Show("33333333");
                    }

                }
            }

            foreach (var forr in fordeclaration)
            {
                var fordeclaration1 = forr.DescendantNodes().OfType<ForStatementSyntax>();

                foreach (var forr1 in fordeclaration1)
                {
                    bool alfa = false;

                    var fordeclaration2 = forr1.DescendantNodes().OfType<ForStatementSyntax>();

                    foreach (var forr2 in fordeclaration2)
                    {
                        alfa = true;
                    }
                    if (alfa)
                    {
                        E_forinfor.Add("moreFor");
                         //MessageBox.Show("Foooor");
                    }

                }
            }
            foreach (var whl in whiledeclaration)
            {
                //MessageBox.Show("Fr");
                var whiledeclaration1 = whl.DescendantNodes().OfType<WhileStatementSyntax>();

                foreach (var whl1 in whiledeclaration1)
                {
                    bool alfa = false;

                    var whiledeclaration2 = whl1.DescendantNodes().OfType<WhileStatementSyntax>();

                    foreach (var whl2 in whiledeclaration2)
                    {
                        alfa = true;
                    }
                    if (alfa)
                    {
                        E_whileinwhile.Add("morewhile");
                        //MessageBox.Show("whileeee");
                    }

                }
            }
            foreach (var foch in forechdeclaration)
            {
                //MessageBox.Show("Fr");
                var forechdeclaration1 = foch.DescendantNodes().OfType<ForEachStatementSyntax>();

                foreach (var foch1 in forechdeclaration1)
                {
                    bool alfa = false;

                    var forechdeclaration2 = foch1.DescendantNodes().OfType<ForEachStatementSyntax>();

                    foreach (var foch2 in forechdeclaration2)
                    {
                        alfa = true;
                    }
                    if (alfa)
                    {
                        E_forchinforch.Add("moreForeche");
                        //MessageBox.Show("whileeee");
                    }

                }
            }


            foreach (var i in classDeclarations)
            {
                class_list.Add(i.Identifier.ToString());
            }

            foreach (var i in methodDeclarations)
            {
                var ret=i.ReturnType;
                string ret1 = ret.ToString();
                //MessageBox.Show(ret1);
                if (ret1!="void") {
                    var aq = i.Identifier;
                    int f = Regex.Matches(code, aq.ToString()).Count;
                    //MessageBox.Show(f.ToString());
                    if (f>=5)
                    {
                        E_methodMoreUse.Add("methodmoreuse");
                    }
                    if (f==1)
                    {
                        E_methodNotUse.Add("methodNotuse");
                    }
                }


                var returnState = i.DescendantNodes().OfType<ReturnStatementSyntax>();

                if (returnState.Count() > 1)
                {
                    E_methodSingel.Add("notsingle");
                    //MessageBox.Show("singleeeeeee");
                }

                var gotodeclaration = i.DescendantNodes().OfType<GotoStatementSyntax>();
                var trycashdeclatarion = i.DescendantNodes().OfType<TryStatementSyntax>();

                string goto1 = "";
                string try1 = "";
                
                foreach (var a in gotodeclaration)
                {
                    goto1 = "a";
                }
                if (goto1.Equals("a"))
                {
                    E_gotoMethod.Add(i.Identifier.ToString());
                    //MessageBox.Show(i.Identifier.ToString());
                }
                foreach(var ol in trycashdeclatarion)
                {
                    try1 = "b";
                }
                if (try1.Equals("b"))
                {
                    E_trymethod.Add(i.Identifier.ToString());
                    //MessageBox.Show(i.Identifier.ToString());
                }
               // MessageBox.Show(i.Identifier.ToString());

                if (i.Identifier.ToString().Equals("main"))
                {

                }
                else
                {
                    method_list.Add(i.Identifier.ToString());
                }
            }
            foreach (var i in fordeclaration)
            {
                var a = i.DescendantNodes().OfType<VariableDeclaratorSyntax>().First();
                // MessageBox.Show(a.Identifier.ToString());
                variabel_in_for.Add(a.Identifier.ToString());
            }

            foreach (var i in variableDeclarator)
            {
                bool alfa = false;

                foreach (var K in variabel_in_for)
                {
                    if (i.Identifier.ToString().Equals(K))
                    {
                        alfa = true;
                    }
                    else
                    {
                     
                    }

                }
                if (alfa)
                {

                }
                else
                {
                    variabel_list.Add(i.Identifier.ToString());
                }
               
   
            }

            foreach (string str in class_list)
            {
                if (str.Length == 1)
                {
                    E_classname.Add(str);
                    continue;
                }
                Boolean first = true;
                Boolean time = false;
                int next = 0;

                string temp = SplitCamelCase(str);

                string[] loghat = temp.Split(' ');

                foreach (var jj in loghat) //baraye sakhtar camelCase
                {
                    if (jj.Length == 1)
                    {
                        time = true;
                    }
                }
                if (time)
                {
                    E_classname.Add(str);
                    continue;
                }

                foreach (var a in loghat)
                {
                    // MessageBox.Show(a);
                    int verb = 0, noun = 0, adj = 0;
                    string target;
                    string word = a;
                    List<SynSet> synSetList = wordNet.GetSynSets(word);
                    foreach (var synSet in synSetList)
                    {

                        var words = string.Join(", ", synSet.Words);

                        string s = synSet.PartOfSpeech.ToString();
                        if (s == "Verb")
                        {
                            verb++;
                        }
                        else if (s == "Noun")
                        {
                            noun++;
                        }
                        else if (s == "Adjective")
                        {
                            adj++;
                        }

                        //MessageBox.Show(str);
                    }
                    target = changer(verb, noun, adj, a);
                    if (target != "Noun" & first)
                    {
                        E_classname.Add(str);
                        first = false;
                    }

                    if (target == "Noun" & first)
                    {
                        //MessageBox.Show("good class");
                        first = false;
                        next = 1;

                    }
                    else if (next == 1)
                    {
                        if (target == "Verb" | target == "Noun")
                        {
                            //MessageBox.Show("good part of class");
                        }
                        else
                        {
                            E_classname.Add(str);
                        }
                    }

                }
            }
            //********************************************************************************
            foreach (string str in variabel_list)
            {
               
                if (str.Length == 1)
                { 
                    E_variabelname.Add(str);
                    continue;
                }
                Boolean first = true;
                Boolean time = false;

                int next = 0;

                string temp = SplitCamelCase(str);

                string[] loghat = temp.Split(' ');

                foreach (var jj in loghat) //baraye sakhtar camelCase
                {
                    if (jj.Length == 1)
                    {
                        time = true;
                    }
                }
                if (time)
                {
                    E_variabelname.Add(str);
                    continue;
                }

                foreach (var a in loghat)
                {
                    int verb = 0, noun = 0, adj = 0;
                    string target;
                    string word = a;
                    List<SynSet> synSetList = wordNet.GetSynSets(word);
                    foreach (var synSet in synSetList)
                    {

                        var words = string.Join(", ", synSet.Words);

                        string s = synSet.PartOfSpeech.ToString();
                        if (s == "Verb")
                        {
                            verb++;
                        }
                        else if (s == "Noun")
                        {
                            noun++;
                        }
                        else if (s == "Adjective")
                        {
                            adj++;
                        }

                    }
                    target = changer(verb, noun, adj, a);

                   // MessageBox.Show(a + " " + verb.ToString() + " " + noun.ToString() + " " + adj.ToString()+" "+target);

                    if (target != "Noun" & first)
                    {
                        E_variabelname.Add(str);
                        first = false;
                    }
                    if (target == "Noun" & first)
                    {
                        first = false;
                        next = 1;
                    }
                    else if (next == 1)
                    {
                        if (target == "Verb" | target == "Noun")
                        {
                            

                        }
                        else
                        {
                            E_variabelname.Add(str);
                        }


                    }
                }

            }
            //***************************************************************************************************
            foreach (string str in method_list)
            {
                if (str.Length == 1)
                {
                    E_methodname.Add(str);
                    continue;
                }
                Boolean first = true;
                Boolean time = false;

                int next = 0;

                string temp = SplitCamelCase(str);

                string[] loghat = temp.Split(' ');
                foreach (var jj in loghat) //baraye sakhtar camelCase
                {
                    if (jj.Length == 1)
                    {
                        time = true;
                    }
                }
                if (time)
                {
                    E_methodname.Add(str);
                    continue;
                }

                foreach (var a in loghat)
                {

                    int verb = 0, noun = 0, adj = 0;
                    string target;
                    string word = a;
                    List<SynSet> synSetList = wordNet.GetSynSets(word);

                    foreach (var synSet in synSetList)
                    {

                        var words = string.Join(", ", synSet.Words);

                        string s = synSet.PartOfSpeech.ToString();
                        if (s == "Verb")
                        {
                            verb++;
                        }
                        else if (s == "Noun")
                        {
                            noun++;
                        }
                        else if (s == "Adjective")
                        {
                            adj++;
                        }

                        //MessageBox.Show(str);
                    }
                    target = changer(verb, noun, adj, a);
                    if (target != "Verb" & first)
                    {
                        E_methodname.Add(str);
                        first = false;
                    }
                    if (target == "Verb" & first)
                    {
                        //MessageBox.Show("good method");
                        first = false;
                        next = 1;

                    }
                    else if (next == 1)
                    {
                        if (target == "Verb" | target == "Noun")
                        {
                            //MessageBox.Show("good part of method");
                        }
                        else
                        {
                            E_methodname.Add(str);
                        }
                    }
                }

            }
            //****************************************************************************************************
            foreach (var u in ifdeclarator)
            {
                var s1 = tree.GetLineSpan(u.OpenParenToken.Span).StartLinePosition.Line;
                var s2 = tree.GetLineSpan(u.CloseParenToken.Span).StartLinePosition.Line;
                //MessageBox.Show((s2 - s1).ToString());
                if ((s2 - s1) != 0)
                {
                    E_ifline.Add("error");
                }

            }

            foreach (var k in whiledeclaration)
            {
                var s1 = tree.GetLineSpan(k.OpenParenToken.Span).StartLinePosition.Line;
                var s2 = tree.GetLineSpan(k.CloseParenToken.Span).StartLinePosition.Line;
                // MessageBox.Show((s2 - s1).ToString());
                if ((s2 - s1) != 0)
                {
                    E_whileline.Add("error");
                }

            }
            foreach (var z in fordeclaration)
            {
                var s1 = tree.GetLineSpan(z.OpenParenToken.Span).StartLinePosition.Line;
                var s2 = tree.GetLineSpan(z.CloseParenToken.Span).StartLinePosition.Line;
                // MessageBox.Show((s2 - s1).ToString());
                if ((s2 - s1) != 0)
                {
                    E_forline.Add("error");
                }
            }
            foreach (var p in forechdeclaration)
            {
                var s1 = tree.GetLineSpan(p.OpenParenToken.Span).StartLinePosition.Line;
                var s2 = tree.GetLineSpan(p.CloseParenToken.Span).StartLinePosition.Line;
                //MessageBox.Show((s2 - s1).ToString());
                if ((s2 - s1) != 0)
                {
                    E_forechlin.Add("error");
                }
            }
            foreach (var i in methodDeclarations1)
            {
                if (i.ParameterList.Parameters.Count > 4)
                {

                    E_paramMethod.Add(i.Identifier.ToString());

                }
            }
            foreach (var i in methodDeclarations1)
            {
                var startLinePosition = tree.GetLineSpan(i.Span).StartLinePosition.Line;
                var endLinePosition = tree.GetLineSpan(i.Span).EndLinePosition.Line;
                if (endLinePosition - startLinePosition + 1 > 24)
                {
                    if (i.Identifier.ToString().Equals("main"))
                    {

                    }
                    else
                    {
                        E_lenMethod.Add(i.Identifier.ToString());
                    }
                }
            }
            foreach (var i in fordeclaration)
            {
                var startLinePosition = tree.GetLineSpan(i.Span).StartLinePosition.Line;
                var endLinePosition = tree.GetLineSpan(i.Span).EndLinePosition.Line;
                if (endLinePosition - startLinePosition + 1 > 4)
                {
                    E_forbodylin.Add("moreThan");
                }
            }
            foreach (var i in ifdeclarator)
            {
                var startLinePosition = tree.GetLineSpan(i.Span).StartLinePosition.Line;
                var endLinePosition = tree.GetLineSpan(i.Span).EndLinePosition.Line;
                if (endLinePosition - startLinePosition + 1 > 4)
                {
                    E_ifbodyline.Add("moreThan");
                }
            }
            foreach (var i in forechdeclaration)
            {
                var startLinePosition = tree.GetLineSpan(i.Span).StartLinePosition.Line;
                var endLinePosition = tree.GetLineSpan(i.Span).EndLinePosition.Line;
                if (endLinePosition - startLinePosition + 1 > 4)
                {
                    E_forechbodyline.Add("moreThan");
                }
            }
            foreach (var i in whiledeclaration)
            {
                var startLinePosition = tree.GetLineSpan(i.Span).StartLinePosition.Line;
                var endLinePosition = tree.GetLineSpan(i.Span).EndLinePosition.Line;
                if (endLinePosition - startLinePosition + 1 > 4)
                {
                    E_whilebodyline.Add("moreThan");
                }
            }
            foreach (var i in ifdeclarator)
            {
                var notelse = i.DescendantNodes().OfType<ElseClauseSyntax>();
                if (notelse.Count()!=0) {

                    var elseClause = i.DescendantNodes().OfType<ElseClauseSyntax>().Last();
                    var startLinePosition = tree.GetLineSpan(elseClause.Span).StartLinePosition.Line;
                    var endLinePosition = tree.GetLineSpan(elseClause.Span).EndLinePosition.Line;

                    if (endLinePosition - startLinePosition + 1 > 4)
                    {
                        E_elsebodyline.Add("moreThan");
                    }
                }
            }

            foreach(var i in E_variabelname)
            {
                alfa = alfa + "--" + i.ToString();
            }
            //MessageBox.Show(alfa);
        }






        public static string SplitCamelCase(string input)
        {
            return System.Text.RegularExpressions.Regex.Replace(input, "([A-Z])", " $1", System.Text.RegularExpressions.RegexOptions.Compiled).Trim();
        }
        string changer(int a, int b, int c, string myword)
        {
            String big = "Verb";
            if (a < c & b < c)
            {
                big = "Noun";
            }
            else if (a < b & c < b)
            {
                big = "Noun";
            }
            else if (a < c & a < b & a == b)
            {
                big = "Noun";
            }
            else if (a == 0 & b == 0 & c == 0)
            {
                big = "Unkown";
                string[] temp = { "my", "My", "your", "Your", "thier", "Thier", "his", "His", "her", "Her" };
                foreach (var m in temp)
                {
                    if (myword == m)
                    {
                        big = "Noun";
                    }
                }
            }
            return big;
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.RowIndex.Equals(0))
            {
                if (dataGridView1.CurrentCell != null && dataGridView1.CurrentCell.Value != null)
                    //MessageBox.Show(dataGridView1.CurrentCell.Value.ToString());
                this.dataGridView1.Rows[0].Cells["Count"].Value = E_variabelname.Count.ToString();
                MessageBox.Show(alfa);
            }
            if (dataGridView1.CurrentCell.RowIndex.Equals(1))
            {
                if (dataGridView1.CurrentCell != null && dataGridView1.CurrentCell.Value != null)
                   // MessageBox.Show(dataGridView1.CurrentCell.Value.ToString());
                this.dataGridView1.Rows[1].Cells["Count"].Value = E_classname.Count.ToString();
            }
            if (dataGridView1.CurrentCell.RowIndex.Equals(2))
            {
                if (dataGridView1.CurrentCell != null && dataGridView1.CurrentCell.Value != null)
                    //MessageBox.Show(dataGridView1.CurrentCell.Value.ToString());
                this.dataGridView1.Rows[2].Cells["Count"].Value = E_methodname.Count.ToString();
            }
            if (dataGridView1.CurrentCell.RowIndex.Equals(3))
            {
                if (dataGridView1.CurrentCell != null && dataGridView1.CurrentCell.Value != null)
                   //MessageBox.Show(dataGridView1.CurrentCell.Value.ToString());
                this.dataGridView1.Rows[3].Cells["Count"].Value = E_ifline.Count.ToString();
            }
            if (dataGridView1.CurrentCell.RowIndex.Equals(4))
            {
                if (dataGridView1.CurrentCell != null && dataGridView1.CurrentCell.Value != null)
                    //MessageBox.Show(dataGridView1.CurrentCell.Value.ToString());
                this.dataGridView1.Rows[4].Cells["Count"].Value = E_forline.Count.ToString();
            }
            if (dataGridView1.CurrentCell.RowIndex.Equals(5))
            {
                if (dataGridView1.CurrentCell != null && dataGridView1.CurrentCell.Value != null)
                    //MessageBox.Show(dataGridView1.CurrentCell.Value.ToString());
                this.dataGridView1.Rows[5].Cells["Count"].Value = E_forechlin.Count.ToString();
            }
            if (dataGridView1.CurrentCell.RowIndex.Equals(6))
            {
                if (dataGridView1.CurrentCell != null && dataGridView1.CurrentCell.Value != null)
                    //MessageBox.Show(dataGridView1.CurrentCell.Value.ToString());
                this.dataGridView1.Rows[6].Cells["Count"].Value = E_whileline.Count.ToString();
            }
            if (dataGridView1.CurrentCell.RowIndex.Equals(7))
            {
                if (dataGridView1.CurrentCell != null && dataGridView1.CurrentCell.Value != null)
                    //MessageBox.Show(dataGridView1.CurrentCell.Value.ToString());
                this.dataGridView1.Rows[7].Cells["Count"].Value = E_ifbodyline.Count.ToString();
            }
            if (dataGridView1.CurrentCell.RowIndex.Equals(8))
            {
                if (dataGridView1.CurrentCell != null && dataGridView1.CurrentCell.Value != null)
                //    MessageBox.Show(dataGridView1.CurrentCell.Value.ToString());
                this.dataGridView1.Rows[8].Cells["Count"].Value = E_elsebodyline.Count.ToString();
            }
            if (dataGridView1.CurrentCell.RowIndex.Equals(9))
            {

                if (dataGridView1.CurrentCell != null && dataGridView1.CurrentCell.Value != null)
                   // MessageBox.Show(dataGridView1.CurrentCell.Value.ToString());
                this.dataGridView1.Rows[9].Cells["Count"].Value = E_forbodylin.Count.ToString();
            }
            if (dataGridView1.CurrentCell.RowIndex.Equals(10))
            {
                if (dataGridView1.CurrentCell != null && dataGridView1.CurrentCell.Value != null)
                    //MessageBox.Show(dataGridView1.CurrentCell.Value.ToString());
                this.dataGridView1.Rows[10].Cells["Count"].Value = E_forechbodyline.Count.ToString();
            }
            if (dataGridView1.CurrentCell.RowIndex.Equals(11))
            {
                if (dataGridView1.CurrentCell != null && dataGridView1.CurrentCell.Value != null)
                    //MessageBox.Show(dataGridView1.CurrentCell.Value.ToString());
                this.dataGridView1.Rows[11].Cells["Count"].Value = E_whilebodyline.Count.ToString();
            }
            if (dataGridView1.CurrentCell.RowIndex.Equals(12))
            {
                    if (dataGridView1.CurrentCell != null && dataGridView1.CurrentCell.Value != null)
                        //MessageBox.Show(dataGridView1.CurrentCell.Value.ToString());
                    this.dataGridView1.Rows[12].Cells["Count"].Value = E_paramMethod.Count.ToString();
            }
             if (dataGridView1.CurrentCell.RowIndex.Equals(13))
             {
                    if (dataGridView1.CurrentCell != null && dataGridView1.CurrentCell.Value != null)
                        //MessageBox.Show(dataGridView1.CurrentCell.Value.ToString());
                    this.dataGridView1.Rows[13].Cells["Count"].Value = E_lenMethod.Count.ToString();

             }
            if (dataGridView1.CurrentCell.RowIndex.Equals(14))
            {
                if (dataGridView1.CurrentCell != null && dataGridView1.CurrentCell.Value != null)
                    //MessageBox.Show(dataGridView1.CurrentCell.Value.ToString());
                this.dataGridView1.Rows[14].Cells["Count"].Value = E_trymethod.Count.ToString();
            }
            if (dataGridView1.CurrentCell.RowIndex.Equals(15))
            {
                if (dataGridView1.CurrentCell != null && dataGridView1.CurrentCell.Value != null)
                   // MessageBox.Show(dataGridView1.CurrentCell.Value.ToString());
                this.dataGridView1.Rows[15].Cells["Count"].Value = E_gotoMethod.Count.ToString();
            }
            if (dataGridView1.CurrentCell.RowIndex.Equals(16))
            {
                if (dataGridView1.CurrentCell != null && dataGridView1.CurrentCell.Value != null)
                   // MessageBox.Show(dataGridView1.CurrentCell.Value.ToString());
                this.dataGridView1.Rows[16].Cells["Count"].Value = E_methodSingel.Count.ToString();
            }
            if (dataGridView1.CurrentCell.RowIndex.Equals(17))
            {
                if (dataGridView1.CurrentCell != null && dataGridView1.CurrentCell.Value != null)
                    //MessageBox.Show(dataGridView1.CurrentCell.Value.ToString());
                this.dataGridView1.Rows[17].Cells["Count"].Value = E_methodNotUse.Count.ToString();
            }
            if (dataGridView1.CurrentCell.RowIndex.Equals(18))
            {
                if (dataGridView1.CurrentCell != null && dataGridView1.CurrentCell.Value != null)
                    //MessageBox.Show(dataGridView1.CurrentCell.Value.ToString());
                    this.dataGridView1.Rows[18].Cells["Count"].Value = E_methodMoreUse.Count.ToString();
            }
            if (dataGridView1.CurrentCell.RowIndex.Equals(19))
            {
                if (dataGridView1.CurrentCell != null && dataGridView1.CurrentCell.Value != null)
                    //MessageBox.Show(dataGridView1.CurrentCell.Value.ToString());
                    this.dataGridView1.Rows[19].Cells["Count"].Value = E_ifinif.Count.ToString();
            }
            if (dataGridView1.CurrentCell.RowIndex.Equals(20))
            {
                if (dataGridView1.CurrentCell != null && dataGridView1.CurrentCell.Value != null)
                    //MessageBox.Show(dataGridView1.CurrentCell.Value.ToString());
                    this.dataGridView1.Rows[20].Cells["Count"].Value = E_forinfor.Count.ToString();
            }
            if (dataGridView1.CurrentCell.RowIndex.Equals(21))
            {
                if (dataGridView1.CurrentCell != null && dataGridView1.CurrentCell.Value != null)
                    //MessageBox.Show(dataGridView1.CurrentCell.Value.ToString());
                    this.dataGridView1.Rows[21].Cells["Count"].Value = E_forchinforch.Count.ToString();
            }
            if (dataGridView1.CurrentCell.RowIndex.Equals(22))
            {
                if (dataGridView1.CurrentCell != null && dataGridView1.CurrentCell.Value != null)
                    //MessageBox.Show(dataGridView1.CurrentCell.Value.ToString());
                    this.dataGridView1.Rows[22].Cells["Count"].Value = E_whileinwhile.Count.ToString();
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Maximum = 100;
            progressBar1.Minimum = 0;

            this.progressBar1.Increment(1);

            if (progressBar1.Value == 100)
            {
                progressBar1.Value = 0;
                timer1.Stop();
            }

        }
    }
}
