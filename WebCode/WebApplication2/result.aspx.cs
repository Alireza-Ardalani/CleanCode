using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using edu.stanford.nlp.tagger.maxent;
using System.Collections;
using edu.stanford.nlp.ling;
using NetSpell.SpellChecker.Dictionary;
using NetSpell.SpellChecker;

namespace WebApplication2
{
    
   
    //variabel Global Address
    static class Src
    {
        public static string fileNameSrc;
        public static DataTable dt;

    }
    public partial class result : System.Web.UI.Page
    {
        public void show(string a1, string a2,string a3, string a4)
        {
            

            Src.dt.Rows.Add( a1, a2,a3 , a4);
            

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            



            string address = @"..\..\inputcode\";
            address += this.Session["id"].ToString();
            Src.fileNameSrc = address;
            Src.dt = new DataTable();
            Src.dt.Columns.Add("  #  ", typeof(string));
            Src.dt.Columns.Add(" methods ", typeof(string));
            Src.dt.Columns.Add(" cleanCode Principle ", typeof(string));
            Src.dt.Columns.Add("count", typeof(string));
            CheckVariableName();
            CheckMethodName();
            CheckMethodParameters();
            CheckMethodSize();
            CheckMethodSingleResponsibility();
            CheckIfBlock();
            CheckElseBlock();
            CheckForBlock();
            CheckForeachBlock();
            
           
            
            CheckWhileBlock();
            CheckNestedIf();
            CheckNestedFor();
            CheckNestedForeach();
            CheckNestedWhile();
            GridView1.DataSource = Src.dt.DefaultView;
            GridView1.DataBind();




        }
        #region splitCamel
        public string SplitCamelCase(string x)
        {
            return System.Text.RegularExpressions.Regex.Replace(x, "([A-Z])", " $1", System.Text.RegularExpressions.RegexOptions.Compiled).Trim();
        }
        #endregion
        #region IsEnglishWord
        public bool IsEnglishWord(string x)
        {
            WordDictionary dictionary = new WordDictionary();
            dictionary.DictionaryFile = @"..\..\Users\IT\source\repos\WebApplication2\WebApplication2\NewFolder1\en-US.dic";
            dictionary.Initialize();
            Spelling spell = new Spelling();
            spell.Dictionary = dictionary;
            if (spell.TestWord(x))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Tag
        public string Tag(string x)
        {
            var model = new MaxentTagger(@"..\..\Users\IT\source\repos\WebApplication2\WebApplication2\NewFolder1\wsj-0-18-bidirectional-nodistsim.tagger", null, false);
            var sentence = MaxentTagger.tokenizeText(new java.io.StringReader(x)).toArray();
            foreach (java.util.ArrayList i in sentence)
            {
                var taggedSentence = model.tagSentence(i);
                return SentenceUtils.listToString(taggedSentence, false);
            }
            return null;
        }
        #endregion
        #region CleanVariableName
        public void CleanVariableName(string x, string y)
        {
            string text1 = SplitCamelCase(x);
            string[] text2 = text1.Split(' ');
            for (int i = 0; i < text2.Length; i++)
            {
                if (IsEnglishWord(text2[i]) != true)
                {
                   // formCheckVariableName.Show(x, y, "It contains a non-English word.");
                    break;
                }
            }
            if (Tag(text2[0]) != text2[0].ToString() + "/NN")
            {
                //formCheckVariableName.Show(x, y, "Its first word is not Noun.");
            }
        }
        #endregion
        #region CleanMethodName
        public void CleanMethodName(string x, string y)
        {
            string text1 = SplitCamelCase(x);
            string[] text2 = text1.Split(' ');
            for (int i = 0; i < text2.Length; i++)
            {
                if (IsEnglishWord(text2[i]) != true)
                {
                   // formCheckMethodName.Show(x, y, "It contains a non-English word.");
                    break;
                }
            }
            if (Tag(text2[0]) != text2[0].ToString() + "/VB")
            {
                //formCheckMethodName.Show(x, y, "Its first word is not Verb.");
            }

        }
        #endregion
        #region CheckVariableName
        public void CheckVariableName()
        {
            int count = 0;
            var tree = CSharpSyntaxTree.ParseText(System.IO.File.ReadAllText(Src.fileNameSrc));
            var root = (CompilationUnitSyntax)tree.GetRoot();
            var variableDeclarator = root.DescendantNodes().OfType<VariableDeclaratorSyntax>();
            var forStatement = root.DescendantNodes().OfType<ForStatementSyntax>();
            java.util.ArrayList arrayList = new java.util.ArrayList();
            foreach (var i in forStatement)
            {
                arrayList.Add(i.DescendantNodes().OfType<IdentifierNameSyntax>().First().Identifier.ToString());
            }
            foreach (var i in variableDeclarator)
            {
                if (!arrayList.contains(i.Identifier.ToString()))
                {
                    count = count + 1;
                    CleanVariableName(i.Identifier.ToString(), tree.GetLineSpan(i.Span).StartLinePosition.Line.ToString());
                }
            }
            show("1", "Variable Name", "All words forming the variable name should be meaningful (English word). The first word forming the variable name should be Noun.", count.ToString());
            
            
        }
        #endregion
        #region CheckMethodName
        public void CheckMethodName()
        {
            int count = 0;
            var tree = CSharpSyntaxTree.ParseText(System.IO.File.ReadAllText(Src.fileNameSrc));
            var root = (CompilationUnitSyntax)tree.GetRoot();
            var methodDeclaration = root.DescendantNodes().OfType<MethodDeclarationSyntax>();
            foreach (var i in methodDeclaration)
            {
                count = count + 1;
                //CleanMethodName(i.Identifier.ToString(), tree.GetLineSpan(i.Span).StartLinePosition.Line.ToString());
            }
            show("2", "Method Name", "All words forming the method name should be meaningful (English word). The first word forming the method name should be Verb.", count.ToString());
        }
        #endregion
        #region CheckMethodParameters
        public void CheckMethodParameters()
        {
            int count = 0;
            var tree = CSharpSyntaxTree.ParseText(System.IO.File.ReadAllText(Src.fileNameSrc));
            var root = (CompilationUnitSyntax)tree.GetRoot();
            var methodDeclaration = root.DescendantNodes().OfType<MethodDeclarationSyntax>();
            foreach (var i in methodDeclaration)
            {
                if (i.ParameterList.Parameters.Count > 4)
                {
                    count = count + 1;
                    //formCheckMethodParameters.Show(i.Identifier.ToString(), tree.GetLineSpan(i.Span).StartLinePosition.Line.ToString());
                }
            }
            show("3", "Method Parameters", "The number of the method parameters should not exceed 4 cases.", count.ToString());
        }
        #endregion
        #region CheckMethodSize
        public void CheckMethodSize()
        {
            int count = 0;
            var tree = CSharpSyntaxTree.ParseText(System.IO.File.ReadAllText(Src.fileNameSrc));
            var root = (CompilationUnitSyntax)tree.GetRoot();
            var methodDeclaration = root.DescendantNodes().OfType<MethodDeclarationSyntax>();
            foreach (var i in methodDeclaration)
            {
                var startLinePosition = tree.GetLineSpan(i.Span).StartLinePosition.Line;
                var endLinePosition = tree.GetLineSpan(i.Span).EndLinePosition.Line;
                if (endLinePosition - startLinePosition + 1 > 24)
                {
                    count = count + 1;
                    //formCheckMethodSize.Show(i.Identifier.ToString(), tree.GetLineSpan(i.Span).StartLinePosition.Line.ToString());
                }
            }
            show("4", "Method Size", "The size of the method should not exceed 24 lines.", count.ToString());
        }
        #endregion
        #region CheckMethodSingleResponsibility
        public void CheckMethodSingleResponsibility()
        {
            int count = 0;
            var tree = CSharpSyntaxTree.ParseText(System.IO.File.ReadAllText(Src.fileNameSrc));
            var root = (CompilationUnitSyntax)tree.GetRoot();
            var methodDeclaration = root.DescendantNodes().OfType<MethodDeclarationSyntax>();
            foreach (var i in methodDeclaration)
            {
                var returnStatement = i.DescendantNodes().OfType<ReturnStatementSyntax>();
                if (returnStatement.Count() > 1)
                {
                    java.util.ArrayList arrayList = new java.util.ArrayList();
                    foreach (var j in returnStatement)
                    {
                        arrayList.add(j.Expression);
                    }
                    for (int k = 1; k < arrayList.size(); k++)
                    {
                        if (arrayList.toArray()[0].ToString() != arrayList.toArray()[k].ToString())
                        {
                            count = count + 1;
                           // formCheckMethodSingleResponsibility.Show(i.Identifier.ToString(), tree.GetLineSpan(i.Span).StartLinePosition.Line.ToString());
                            break;
                        }
                    }
                }
            }
            show("5", "Single Responsibility", "The method should have one responsibility (single responsibility principle).", count.ToString());
        }
        #endregion
        #region ChechIfBlock
        public void CheckIfBlock()
        {
            int count = 0;
            var tree = CSharpSyntaxTree.ParseText(System.IO.File.ReadAllText(Src.fileNameSrc));
            var root = (CompilationUnitSyntax)tree.GetRoot();
            var ifStatement = root.DescendantNodes().OfType<IfStatementSyntax>();
            
            foreach (var i in ifStatement)
            { 
                //var elseClause = i.DescendantNodes().OfType<ElseClauseSyntax>().First();
                var startLinePosition = tree.GetLineSpan(i.Span).StartLinePosition.Line;
                 var endLinePosition = tree.GetLineSpan(i.Span).StartLinePosition.Line - 1;
                // var endLinePosition = tree.GetLineSpan(elseClause.Span).StartLinePosition.Line - 1;
                if (endLinePosition - startLinePosition + 1 != 4)
                    {
                        count = count + 1;
                       // formCheckIfBlock.Show(tree.GetLineSpan(i.Span).StartLinePosition.Line.ToString());
                    }
            }
            show("6", "If Block", "The block within If statement should be 1 line. Probably that line should be a function call.", count.ToString());
        }
        #endregion
        #region CheckElseBlock
        public void CheckElseBlock()
        {
            int count = 0;
            java.util.ArrayList arrayList = new java.util.ArrayList();
            var tree = CSharpSyntaxTree.ParseText(System.IO.File.ReadAllText(Src.fileNameSrc));
            var root = (CompilationUnitSyntax)tree.GetRoot();
            var ifStatement = root.DescendantNodes().OfType<IfStatementSyntax>();
            foreach (var i in ifStatement)
            {
                var notelse = i.DescendantNodes().OfType<ElseClauseSyntax>();
                if (notelse.Count() != 0)
                {
                    var elseClause = i.DescendantNodes().OfType<ElseClauseSyntax>().Last();
                    var startLinePosition = tree.GetLineSpan(elseClause.Span).StartLinePosition.Line;
                    var endLinePosition = tree.GetLineSpan(elseClause.Span).EndLinePosition.Line;
                    if (endLinePosition - startLinePosition + 1 != 4)
                    {
                        if (arrayList.contains(tree.GetLineSpan(elseClause.Span).StartLinePosition.Line.ToString()) != true)
                        {
                            count = count + 1;
                            arrayList.add(tree.GetLineSpan(elseClause.Span).StartLinePosition.Line.ToString());
                            //formCheckElseBlock.Show(tree.GetLineSpan(elseClause.Span).StartLinePosition.Line.ToString());
                        }
                    }
                }
            }
                show("7", "Else Block", "The block within Else statement should be 1 line. Probably that line should be a function call.", count.ToString());
            
        }
        #endregion
        #region CheckForBlock
        public void CheckForBlock()
        {
            int count = 0;
            var tree = CSharpSyntaxTree.ParseText(System.IO.File.ReadAllText(Src.fileNameSrc));
            var root = (CompilationUnitSyntax)tree.GetRoot();
            var forStatement = root.DescendantNodes().OfType<ForStatementSyntax>();
            foreach (var i in forStatement)
            {
                var startLinePosition = tree.GetLineSpan(i.Span).StartLinePosition.Line;
                var endLinePosition = tree.GetLineSpan(i.Span).EndLinePosition.Line;
                if (endLinePosition - startLinePosition + 1 != 4)
                {
                    count = count + 1;
                    //formCheckForBlock.Show(tree.GetLineSpan(i.Span).StartLinePosition.Line.ToString());
                }
            }
            show("8", "For Block", "The block within For statement should be 1 line. Probably that line should be a function call.", count.ToString());
        }
        #endregion
        #region CheckForeachBlock
        public void CheckForeachBlock()
        {
            int count = 0;
            var tree = CSharpSyntaxTree.ParseText(System.IO.File.ReadAllText(Src.fileNameSrc));
            var root = (CompilationUnitSyntax)tree.GetRoot();
            var foreachStatement = root.DescendantNodes().OfType<ForEachStatementSyntax>();
            foreach (var i in foreachStatement)
            {
                var startLinePosition = tree.GetLineSpan(i.Span).StartLinePosition.Line;
                var endLinePosition = tree.GetLineSpan(i.Span).EndLinePosition.Line;
                if (endLinePosition - startLinePosition + 1 != 4)
                {
                    count = count + 1;
                    //formCheckForBlock.Show(tree.GetLineSpan(i.Span).StartLinePosition.Line.ToString());
                }
            }
            show("9", "Foreach Block", "The block within Foreach statement should be 1 line. Probably that line should be a function call.", count.ToString());
        }
        #endregion
        #region CheckWhileBlock
        public void CheckWhileBlock()
        {
            int count = 0;
            var tree = CSharpSyntaxTree.ParseText(System.IO.File.ReadAllText(Src.fileNameSrc));
            var root = (CompilationUnitSyntax)tree.GetRoot();
            var whileStatement = root.DescendantNodes().OfType<WhileStatementSyntax>();
            foreach (var i in whileStatement)
            {
                var startLinePosition = tree.GetLineSpan(i.Span).StartLinePosition.Line;
                var endLinePosition = tree.GetLineSpan(i.Span).EndLinePosition.Line;
                if (endLinePosition - startLinePosition + 1 != 4)
                {
                    count = count + 1;
                   // formCheckWhileBlock.Show(tree.GetLineSpan(i.Span).StartLinePosition.Line.ToString());
                }
            }
            show("10", "While Block", "The block within While statement should be 1 line. Probably that line should be a function call.", count.ToString());
        }
        #endregion
        #region CheckNestedIf
        public void CheckNestedIf()
        {
            int count = 0;
            var tree = CSharpSyntaxTree.ParseText(System.IO.File.ReadAllText(Src.fileNameSrc));
            var root = (CompilationUnitSyntax)tree.GetRoot();
            var ifStatement1 = root.DescendantNodes().OfType<IfStatementSyntax>();
            foreach (var i in ifStatement1)
            {
                var ifStatement2 = i.DescendantNodes().OfType<IfStatementSyntax>();
                foreach (var j in ifStatement2)
                {
                    var ifStatement3 = j.DescendantNodes().OfType<IfStatementSyntax>();
                    if (ifStatement3.Count() > 0)
                    {
                        count = count + 1;
                        //formCheckNestedIf.Show(tree.GetLineSpan(i.Span).StartLinePosition.Line.ToString() + " ---> " + tree.GetLineSpan(j.Span).StartLinePosition.Line.ToString() + " ---> " + tree.GetLineSpan(ifStatement3.First().Span).StartLinePosition.Line.ToString());
                    }
                }
            }
            show("11", "Nested If", "The nested If statements should not exceed 2 levels.", count.ToString());
        }
        #endregion
        #region CheckNestedFor
        public void CheckNestedFor()
        {
            int count = 0;
            var tree = CSharpSyntaxTree.ParseText(System.IO.File.ReadAllText(Src.fileNameSrc));
            var root = (CompilationUnitSyntax)tree.GetRoot();
            var forStatement1 = root.DescendantNodes().OfType<ForStatementSyntax>();
            foreach (var i in forStatement1)
            {
                var forStatement2 = i.DescendantNodes().OfType<ForStatementSyntax>();
                foreach (var j in forStatement2)
                {
                    var forStatement3 = j.DescendantNodes().OfType<ForStatementSyntax>();
                    if (forStatement3.Count() > 0)
                    {
                        count = count + 1;
                        //formCheckNestedFor.Show(tree.GetLineSpan(i.Span).StartLinePosition.Line.ToString() + " ---> " + tree.GetLineSpan(j.Span).StartLinePosition.Line.ToString() + " ---> " + tree.GetLineSpan(forStatement3.First().Span).StartLinePosition.Line.ToString());
                    }
                }
            }
            show("12", "Nested For", "The nested For statements should not exceed 2 levels.", count.ToString());
        }
        #endregion
        #region CheckNestedForeach
        public void CheckNestedForeach()
        {
            int count = 0;
            var tree = CSharpSyntaxTree.ParseText(System.IO.File.ReadAllText(Src.fileNameSrc));
            var root = (CompilationUnitSyntax)tree.GetRoot();
            var forStatementeach1 = root.DescendantNodes().OfType<ForEachStatementSyntax>();
            foreach (var i in forStatementeach1)
            {
                var foreachStatement2 = i.DescendantNodes().OfType<ForEachStatementSyntax>();
                foreach (var j in foreachStatement2)
                {
                    var foreachStatement3 = j.DescendantNodes().OfType<ForEachStatementSyntax>();
                    if (foreachStatement3.Count() > 0)
                    {
                        count = count + 1;
                        //formCheckNestedFor.Show(tree.GetLineSpan(i.Span).StartLinePosition.Line.ToString() + " ---> " + tree.GetLineSpan(j.Span).StartLinePosition.Line.ToString() + " ---> " + tree.GetLineSpan(forStatement3.First().Span).StartLinePosition.Line.ToString());
                    }
                }
            }
            show("13", "Nested Foreach", "The nested Foreach statements should not exceed 2 levels.", count.ToString());
        }
        #endregion
        #region CheckNestedWhile
        public void CheckNestedWhile()
        {
            int count = 0;
            var tree = CSharpSyntaxTree.ParseText(System.IO.File.ReadAllText(Src.fileNameSrc));
            var root = (CompilationUnitSyntax)tree.GetRoot();
            var whileStatement1 = root.DescendantNodes().OfType<WhileStatementSyntax>();
            foreach (var i in whileStatement1)
            {
                var whileStatement2 = i.DescendantNodes().OfType<WhileStatementSyntax>();
                foreach (var j in whileStatement2)
                {
                    var whileStatement3 = j.DescendantNodes().OfType<WhileStatementSyntax>();
                    if (whileStatement3.Count() > 0)
                    {
                        count = count + 1;
                        //formCheckNestedWhile.Show(tree.GetLineSpan(i.Span).StartLinePosition.Line.ToString() + " ---> " + tree.GetLineSpan(j.Span).StartLinePosition.Line.ToString() + " ---> " + tree.GetLineSpan(whileStatement3.First().Span).StartLinePosition.Line.ToString());
                    }
                }
            }
            show("14", "Nested While", "The nested While statements should not exceed 2 levels.", count.ToString());
        }
        #endregion

    }
}