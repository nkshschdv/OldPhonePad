using System.Text;

namespace OldPadPhone
{
    public static class OldPadPhone
    {
        private static readonly Dictionary<char, string> Dic = new Dictionary<char, string>() {
            {'1', "&'("}, {'2', "ABC"}, {'3', "DEF"},
            {'4', "GHI"}, {'5', "JKL"}, {'6', "MNO"},
            {'7', "PQRS"}, {'8', "TUV"}, {'9', "WXYZ"}
        };
        public static string OldPhonePad(string userInput)
        {
            if (!userInput.EndsWith('#'))
            {
                userInput += '#';
            }
            StringBuilder finalResult = new StringBuilder();
            char previous = ' ';
            int count = 0;

            foreach (char currentKey in userInput)
            {
                if (currentKey == '#')
                    break;
                switch (currentKey)
                {
                    case '*':
                        if (finalResult.Length > 0)
                            finalResult.Remove(finalResult.Length - 1, 1); //Remove Last character
                        previous = currentKey;
                        break;
                    case ' ':
                        previous = currentKey;
                        break;
                    case '0': // Each zero output single space
                        finalResult.Append(" ");
                        previous = currentKey;
                        break;
                    default:
                        if (Dic.ContainsKey(currentKey)) // Ignore non dictionary characters
                        {
                            if (currentKey == previous)
                            {
                                finalResult.Remove(finalResult.Length - 1, 1); //Remove last outdated count of character
                                finalResult.Append(Dic[currentKey][count % Dic[currentKey].Length]); // Append with latest count of character 
                                count++;
                            }
                            else
                            {
                                // append first character of a pressed keypad 
                                finalResult.Append(Dic[currentKey][0]);
                                previous = currentKey;
                                count = 1;
                            }
                        } 
                        break;
                }
            }
            return finalResult.ToString();
        }

        public static void Main(string[] args)
        {
            Console.WriteLine(OldPhonePad("33#")); // E
            Console.WriteLine(OldPhonePad("227*#")); //B
            Console.WriteLine(OldPhonePad("4433555 555666#")); // HELLO
            Console.WriteLine(OldPhonePad("8 88777444666*664#")); // TURING 
        }
    }
}
