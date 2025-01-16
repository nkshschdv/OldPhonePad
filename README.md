# Problem Statement
---

---

Here is an old phone keypad with alphabetical letters, a backspace key, and a send button.

Each button has a number to identify it, and pressing a button multiple times will cycle through the letters on it, allowing each button to represent more than one letter.

For example, pressing `1` once will return `A`, pressing it twice in succession will return `B`, and pressing it three times will return `C`.

You must pause for a second in order to type two characters from the same button after each other: `111 1 11` -> `CAB`.


### Keypad Layout

| Input  | Output                |
|--------|-----------------------|
| `1`    | &                     |
| `11`   | '                     |
| `111`  | (                     |
| `2`    | A                     |
| `22`   | B                     |
| `222`  | C                     |
| `3`    | D                     |
| `33`   | E                     |
| `333`  | F                     |
| `4`    | G                     |
| `44`   | H                     |
| `444`  | I                     |
| `5`    | J                     |
| `55`   | K                     |
| `555`  | L                     |
| `6`    | M                     |
| `66`   | N                     |
| `666`  | O                     |
| `7`    | P                     |
| `77`   | Q                     |
| `777`  | R                     |
| `7777` | S                     |
| `8`    | T                     |
| `88`   | U                     |
| `888`  | V                     |
| `9`    | W                     |
| `99`   | X                     |
| `999`  | Y                     |
| `9999` | Z                     |
| `0`    | (space)               |
| `*`    | Delete Last Character |
| `#`    | End of Input          |



### 🔢 Example

| Example                       | Output   | Explanation                                                                 |
|-------------------------------|----------|-----------------------------------------------------------------------------|
| `33#`                         | E        | `33` -> `E`, `#` indicates the end of input. Final -> `E`                  |
| `227*#`                       | B        | `22` -> `B`, `7` -> `P`, `*` removes `P`. Final -> `B`                     |
| `4433555 555666#`             | HELLO    | `44` -> `H`, `33` -> `E`, `555` -> `L`, (pause), `555` -> `L`, `666` -> `O`. Final -> `HELLO` |
| `8 88777444666*664#`          | TURING   | `8` -> `T`, (pause), `88` -> `U`, `777` -> `R`, `444` -> `I`, `666` -> `O`, `*` removes `O`, `66` -> `N`, `4` -> `G`. Final -> `TURING` |

### Task:

Please design and document a class or method that will turn any input to **OldPhonePad** into the correct output.

Assume that a send `#` will always be included at the end of every input.

### Tested Additional User Inputs 

| Example                       | Output   |                                                  |
|-------------------------------|----------|
| `777*666* 555*42633#`     | GAME              
| `***********#`                       | (empty)        | 
| `22266655530*****44402220330222882233#`             | I C E CUBE    |
| `2222 0 220 0 222222#`                       | A B  C        | 


### Solution

```csharp
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

````