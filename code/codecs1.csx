public static string MyFirstInterpreter (string code)
{
    byte cell = 0;
    var codeList = code.ToCharArray ();
    var outputBuilder = new StringBuilder ();
    foreach (var instr in codeList)
    {
        switch (instr)
        {
            case '+':
                {
                    cell += 1;
                    break;
                }
            case '.':
                {
                    outputBuilder.Append (Convert.ToChar (cell));
                    break;
                }
            default:
                {
                    throw new Exception ("Invalid path!");
                }
        }
    }
    return outputBuilder.ToString ();
}