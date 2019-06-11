using System;
using System.IO;
using System.Collections.Generic;

namespace NarrativeFrame
{
    class MainGame
    {

        static string[] textFile;
        static Story mainStory;
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("~Welcome to ham narrative frame!~");
            Console.Write("\n\nEnter story file name: ");
            string fileName = Console.ReadLine();
            
            LoadStory(fileName + ".txt");
            mainStory.StartStory();
        }

        static void LoadStory(string fileName){
            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    Console.Clear();
                    Console.WriteLine("Loading story: " + fileName);
                    mainStory = new Story();
                    List<StoryNode> jumpPoints = new List<StoryNode>();
                    List<StoryNode> jumpStarts = new List<StoryNode>();
                    String line = sr.ReadToEnd();
                    textFile = line.Split(new string[] {"\n", "\r\n"}, StringSplitOptions.RemoveEmptyEntries);
                    //Console.WriteLine(textFile.Length);
                    for(int lineIndex = 0; lineIndex < textFile.Length; lineIndex++){
                        StoryNode newNode = mainStory.AddNode();
                        string text = textFile[lineIndex];
                        //Clean Text first
                        //Find level
                        //Give upper node option text
                        //Look for @, >
                        int atSymbol = text.IndexOf('@');
                        int arrowSymbol = text.IndexOf('>');
                        int level = atSymbol / 4;
                        
                        

                        string nodeText = text.Substring(arrowSymbol + 1);
                        string promptText = text.Substring(atSymbol + 1, arrowSymbol - atSymbol - 1);
                        //Console.WriteLine("text: " + nodeText + ", prompt: " + promptText + ", level: " + level);

                        string a = nodeText.Substring(nodeText.Length -3, 1);
                        if(a.Equals("*")){
                            //store the jump point
                            jumpPoints.Add(newNode);
                            int refer = int.Parse(nodeText.Substring(nodeText.Length - 2, 2));
                            newNode.SetJumpRef(refer);
                            //take off the last 3 characters
                            nodeText = nodeText.Substring(0, nodeText.Length-3);
                        }

                        newNode.SetText(nodeText);
                        newNode.SetPrompt(promptText);
                        newNode.SetLevel(level);
                        
                        //link somehow
                        if(lineIndex != 0){
                            for(int i = lineIndex; i >= 0; i--){
                                StoryNode nodeAbove = mainStory.GetNodeAtIndex(i);
                                if(nodeAbove.GetLevel() < level){ //TODO this links too many things
                                    nodeAbove.AddLink(newNode);
                                    //Console.WriteLine(newNode.GetLevel() + " linked to " + nodeAbove.GetLevel());
                                    break;
                                }
                            }
                        }
                        if(nodeText.Length > 4){
                            if(nodeText.Substring(0, 3).Equals("jmp")){
                                jumpStarts.Add(newNode);
                            }
                        }
                    }
                    //Link Jump Points
                    foreach(var start in jumpStarts){
                        string jumpNum = start.GetText().Substring(3,2);
                        foreach(var end in jumpPoints){
                            if(end.GetJumpRef() == int.Parse(jumpNum)){
                                start.SetJumpPoint(end);
                            }
                        }
                    }
                }
            }
            catch (FileNotFoundException ioEx)
            {
                Console.WriteLine(ioEx.Message);
            }
        }
    }
}
