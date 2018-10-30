open System
open System.CodeDom
open System.CodeDom.Compiler
open System.ComponentModel
open System.IO
open System.Windows.Forms
open System.Drawing

type TestForm() as this =
    inherit Form()
    let textBox1 = new TextBox()
    let button1 = new Button()
    
    do this.SuspendLayout()
       textBox1.Anchor <- (((AnchorStyles.Top ||| AnchorStyles.Bottom) ||| AnchorStyles.Left) ||| AnchorStyles.Right);
       textBox1.Location <- Point(8, 40)
       textBox1.Multiline <- true
       textBox1.Name <- "textBox1"
       textBox1.Size <- Size(391, 242)
       textBox1.TabIndex <- 0
       textBox1.Text <- ""
       button1.Location <- Point(11, 8)
       button1.Name <- "button1"
       button1.Size <- Size(229, 23)
       button1.TabIndex <- 1
       button1.Text <- "Generate string using IndentedTextWriter"
       button1.Click.AddHandler(EventHandler(fun sender e -> this.button1_Click(sender, e)))
       this.AutoScaleBaseSize <- Size(5, 13)
       this.ClientSize <- Size(407, 287)
       this.Controls.Add(button1)
       this.Controls.Add(textBox1)
       this.Name <- "TestForm"
       this.Text <- "IndentedTextWriter example"
       this.ResumeLayout(false)

    member private this.CreateMultilevelIndentString() =
        use baseTextWriter = new StringWriter()
        use indentWriter = new IndentedTextWriter(baseTextWriter, "    ")
        indentWriter.Indent <- 0
        this.WriteLevel(indentWriter, 0, 5)
        baseTextWriter.ToString()

    member private this.WriteLevel(indentWriter: IndentedTextWriter, level: int, totalLevels: int) =
        indentWriter.WriteLine("This is a test phrase. Current indentation level: " + level.ToString())
        if level < totalLevels then
            indentWriter.Indent <- indentWriter.Indent + 1
            this.WriteLevel(indentWriter, level + 1, totalLevels)
            indentWriter.Indent <- indentWriter.Indent - 1
        else
            indentWriter.WriteLineNoTabs("This is a test phrase written with the IndentTextWriter.WriteLineNoTabs method.")
        indentWriter.WriteLine("This is a test phrase. Current indentation level: " + level.ToString())

    member this.button1_Click(sender: obj, e: EventArgs) =
        textBox1.Text <- this.CreateMultilevelIndentString()