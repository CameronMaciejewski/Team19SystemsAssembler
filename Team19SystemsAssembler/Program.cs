using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team19SystemsAssembler
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\jsnyde\230p4\assemblerInput.txt");

            string[] outputLines;
            List<string> outputLineList = new List<string>();

            outputLineList.Add("WIDTH=24;");
            outputLineList.Add("DEPTH=1024;");
            outputLineList.Add("ADDRESS_RADIX=UNS;");
            outputLineList.Add("DATA_RADIX=BIN;");
            outputLineList.Add("CONTENT BEGIN");
            outputLineList.Add("0 : 000000000000000000000000;");
            
            for (int i = 0; i < lines.Length; i++)
            {
                char[] command = new char[24];
                command = generateCommand(lines[i]);
                string commandString = new string(command);
                StringBuilder sb = new StringBuilder();
                sb.Append(i+1);
                sb.Append(" : ");
                sb.Append(commandString);
                sb.Append(";");
                outputLineList.Add(sb.ToString());

            }
            if (lines.Length < 1024){
                StringBuilder sb2 = new StringBuilder();
                sb2.Append("[");
                sb2.Append(lines.Length + 1);
                sb2.Append("..1023] : 000000000000000000000000;");
                outputLineList.Add(sb2.ToString());
            }
            outputLineList.Add("END;");
            outputLines = outputLineList.ToArray<String>();
           
            //System.IO.File.WriteAllText(@"C:\Users\jsnyde\230p4\MemoryInitialization.mif", "");
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\jsnyde\230p4\MemoryInitialization.mif"))
           {
                foreach (string line in outputLines)
                {
                       file.WriteLine(line);
                }
            }
        }
        private static char[] generateCommand(string line)
        {
            char[] command = new char[24];
            string[] commandParts = line.Split(' ');
            char instructionType;
            string tempOpCode = "0000";
            char[] opCode = tempOpCode.ToCharArray();
            #region Init
            switch (commandParts[0])
            {
                case "bne":
                    instructionType = 'B';
                    break;
                case "beq":
                    instructionType = 'B';
                    break;
                case "bgt":
                    instructionType = 'B';
                    break;
                case "blt":
                    instructionType = 'B';
                    break;
                case "bge":
                    instructionType = 'B';
                    break;
                case "ble":
                    instructionType = 'B';
                    break;
                case "add":
                    instructionType = 'R';
                    break;
                case "sub":
                    instructionType = 'R';
                    break;
                case "and":
                    instructionType = 'R';
                    break;
                case "or":
                    instructionType = 'R';
                    break;
                case "xor":
                    instructionType = 'R';
                    break;
                case "cmp":
                    instructionType = 'R';
                    break;
                case "jr":
                    instructionType = 'R';
                    break;
                case "addi":
                    instructionType = 'D';
                    break;
                case "ldw":
                    instructionType = 'D';
                    break;
                case "stw":
                    instructionType = 'D';
                    break;
                case "rand":
                    instructionType = 'X';
                    break;
                default:
                    instructionType = 'E';
                    break;                
            }
            #endregion B
            switch (instructionType)
            {
                #region B
                case 'B':
                    tempOpCode = "0010";
                    opCode = tempOpCode.ToCharArray();
                    string tempCond;
                    char[] cond = new char[4];
                    int label = int.Parse(commandParts[1]);
                    switch (commandParts[0])
                    {
                        case "bne":
                            tempCond = "1100";
                            cond = tempCond.ToCharArray();
                            break;
                        case "beq":
                            tempCond = "0100";
                            cond = tempCond.ToCharArray();
                            break;
                        case "bgt":
                            tempCond = "0011";
                            cond = tempCond.ToCharArray();
                            break;
                        case "blt":
                            tempCond = "1011";
                            cond = tempCond.ToCharArray();
                            break;
                        case "bge":
                            tempCond = "0111";
                            cond = tempCond.ToCharArray();
                            break;
                        case "ble":
                            tempCond = "1111";
                            cond = tempCond.ToCharArray();
                            break;
                    }
                    char[] labelChars = decimalToBinary(label, 16).ToCharArray();
                    for (int i = 0; i < 4; i++)
                    {
                        command[19 - i] = cond[i];
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        command[i] = labelChars[i];
                    }
                    break;
                #endregion B B
                #region R
                case 'R':
                    string tempOPx;
                    char[] opx = new char[3];
                    char s = '0';
                    cond = new char[]{'0', '0', '0','0'};

                    int regDNum;
                    int regSNum;
                    int regTNum;
                    char[] regD = new char[4];
                    char[] regT = new char[4];
                    char[] regS = new char[4];
                    #region
                    switch (commandParts[0])
                    {
                        case "add":
                            tempOpCode = "0011";
                            opCode = tempOpCode.ToCharArray();
                            tempOPx = "110";
                            opx = tempOPx.ToCharArray();
                            break;
                        case "sub":
                            tempOpCode = "0011";
                            opCode = tempOpCode.ToCharArray();
                            tempOPx = "001";
                            opx = tempOPx.ToCharArray();
                            break;
                        case "and":
                            tempOpCode = "0011";
                            opCode = tempOpCode.ToCharArray();
                            tempOPx = "000";
                            opx = tempOPx.ToCharArray();
                            break;
                        case "or":
                            tempOpCode = "0011";
                            opCode = tempOpCode.ToCharArray();
                            tempOPx = "100";
                            opx = tempOPx.ToCharArray();
                            break;
                        case "xor":
                            tempOpCode = "0011";
                            opCode = tempOpCode.ToCharArray();
                            tempOPx = "010";
                            opx = tempOPx.ToCharArray();
                            break;
                        case "cmp":
                            tempOpCode = "0111";
                            opCode = tempOpCode.ToCharArray();
                            tempOPx = "000";
                            opx = tempOPx.ToCharArray();
                            s = '1';
                            break;
                        case "jr":
                            tempOpCode = "1011";
                            opCode = tempOpCode.ToCharArray();
                            tempOPx = "000";
                            opx = tempOPx.ToCharArray();
                            break;
                    }
                    #endregion
                    regDNum = int.Parse(commandParts[1].Replace("r", ""));
                    regSNum = int.Parse(commandParts[2].Replace("r", ""));
                    regTNum = int.Parse(commandParts[3].Replace("r", ""));
                    regD = fourBitBinary(regDNum).ToCharArray();
                    regS = fourBitBinary(regSNum).ToCharArray();
                    regT = fourBitBinary(regTNum).ToCharArray();
                    for (int i = 0; i < 3; i++)
                    {
                        command[14 - i] = opx[i];
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        command[19 - i] = cond[i];
                        command[8 + i] = regD[i];
                        command[4 + i] = regS[i];
                        command[i] = regT[i];
                    }
                    command[15] = s;
                    break;
                #endregion
                #region D
                case 'D':
                    int regSNumD;
                    int regTNumD;
                    char[] regTD = new char[4];
                    char[] regSD = new char[4];
                    regTNumD = int.Parse(commandParts[1].Replace("r", ""));
                    string[] partsOfCommandParts;
                    int immediate = 0;
                    switch(commandParts[0])
                    {
                        case "addi":
                            tempOpCode = "0101";
                            opCode = tempOpCode.ToCharArray();
                            regSNumD = int.Parse(commandParts[2].Replace("r", ""));
                            regSD = fourBitBinary(regSNumD).ToCharArray();
                            regTD = fourBitBinary(regTNumD).ToCharArray();
                            immediate = int.Parse(commandParts[3]);
                            break;
                        case "ldw":
                            tempOpCode = "0001";
                            opCode = tempOpCode.ToCharArray();

                            partsOfCommandParts = commandParts[2].Split(')');
                            regSNumD = int.Parse(partsOfCommandParts[1].Replace("r", ""));
                            immediate = int.Parse(partsOfCommandParts[0].Replace("(", ""));
                            break;
                        case "stw":
                            tempOpCode = "1001";
                            opCode = tempOpCode.ToCharArray();

                            partsOfCommandParts = commandParts[2].Split(')');

                            regSNumD = int.Parse(partsOfCommandParts[1].Replace("r", ""));
                            immediate = int.Parse(partsOfCommandParts[0].Replace("(", ""));
                            break;
                        default:
                            tempOpCode = "0000";
                            opCode = tempOpCode.ToCharArray();
                            break;

                    }
                    cond = new char[]{'0', '0', '0', '0'};
                    s = '0';
                    command[15] = s;
                    char[] immediateChars = decimalToBinary(immediate, 7).ToCharArray();                 
                    for (int i = 0; i < 7; i++)
                    {
                        command[8 + i] = immediateChars[i];
                    }

                    for (int i = 0; i < 4; i++)
                    {
                        command[4 + i] = regSD[i];
                        command[i] = regTD[i];
                        command[19 - i] = cond[i];
                    }
                    break;
                #endregion
                #region X(Random)
                case 'X':
                    opCode = new char[] { '1', '0', '0', '0' };
                    char[] regRand = new char[4];
                    regRand = fourBitBinary(int.Parse(commandParts[1].Replace("r", ""))).ToCharArray();
                    for (int i = 0; i < 24; i++)
                    {
                        command[i] = '0';
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        command[8 + i] = regRand[i];
                    }
                    break;
                #endregion
                default:
                    tempOpCode = "0000";
                    opCode = tempOpCode.ToCharArray();
                    break;
            }
            for (int i = 0; i < 4; i++)
            {
                command[23 - i] = opCode[i];
            }
            return command;

        }
        private static string fourBitBinary(int x)
        {
            string binary = "";

            switch (x)
            {
                case 0:
                    binary = "0000";
                    break;
                case 1:
                    binary = "0001";
                    break;
                case 2:
                    binary = "0010";
                    break;
                case 3:
                    binary = "0011";
                    break;
                case 4:
                    binary = "0100";
                    break;
                case 5:
                    binary = "0101";
                    break;
                case 6:
                    binary = "0110";
                    break;
                case 7:
                    binary = "0111";
                    break;
                case 8:
                    binary = "1000";
                    break;
                case 9:
                    binary = "1001";
                    break;
                case 10:
                    binary = "1010";
                    break;
                case 11:
                    binary = "1011";
                    break;
                case 12:
                    binary = "1100";
                    break;
                case 13:
                    binary = "1101";
                    break;
                case 14:
                    binary = "1110";
                    break;
                case 15:
                    binary = "1111";
                    break;

            }

            return binary;
        }
        static private string decimalToBinary(int decNum, int numBits)
        {
            int remainder;
            string binaryNum = string.Empty;
            Boolean negative;

            if (decNum >= 0)
            {
                negative = false;
            }
            else
            {
                negative = true;
                decNum = Math.Abs(decNum);
            }

            while (decNum != 0)
            {
                remainder = decNum % 2;
                decNum /= 2;
                binaryNum = remainder.ToString() + binaryNum;
            }

            binaryNum = binaryNum.PadLeft(numBits, '0');


            if (negative)
            {
                char[] binaryArray = binaryNum.ToCharArray();
                Boolean hitFirst1 = false;
                for (int i = binaryArray.Length - 1; i >= 0; i--)
                {
                    if (binaryArray[i] == '1' && hitFirst1)
                    {
                        binaryArray[i] = '0';
                    }
                    else if (binaryArray[i] == '0' && hitFirst1)
                    {
                        binaryArray[i] = '1';
                    }
                    else if (binaryArray[i] == '1' && !hitFirst1)
                    {
                        binaryArray[i] = '1';
                        hitFirst1 = true;
                    }
                    else if (binaryArray[i] == '0' && !hitFirst1)
                    {
                        binaryArray[i] = '0';
                    }

                }

                binaryNum = new string(binaryArray);
            }
            return binaryNum;
        }
    }
}
