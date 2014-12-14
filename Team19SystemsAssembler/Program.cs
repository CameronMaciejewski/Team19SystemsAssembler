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

            }
        }
        private static char[] generateCommand(string line)
        {
            char[] command = new char[24];
            string[] commandParts = line.Split(' ');
            char instructionType;
            string tempOpCode;
            char[] opCode;
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
                default:
                    instructionType = 'E';
                    break;                
            }
            switch (instructionType)
            {
                #region
                case 'B':
                    tempOpCode = "0010";
                    opCode = tempOpCode.ToCharArray();
                    string tempCond;
                    char[] cond = new char[4];
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
                    for (int i = 0; i < 4; i++)
                    {
                        command[19 - i] = cond[i];
                    }
                    break;
                #endregion B
                #region 
                case 'R':
                    string tempOPx;
                    char[] opx = new char[3];
                    char s = '0';
                    cond = new char[]{'0', '0', '0','0'};
                    string tempRegD;
                    string tempRegS;
                    string tempRegT;
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
                            s = '1'
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
                    
                    for (int i = 0; i < 3; i++)
                    {
                        command[14 - i] = opx[i];
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        command[19 - i] = cond[i];
                    }
                    command[15] = s;
                    break;
                #endregion
                case 'D':
                    tempOpCode = "0010";
                    opCode = tempOpCode.ToCharArray();
                    break;
                default:
                    tempOpCode = "0000";
                    opCode = tempOpCode.ToCharArray();
                    break;
            }
            for (int i = 0; i < 4; i++)
            {
                command[23 - i] = opCode[i];
            }

        }
    }
}
