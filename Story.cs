using System;
using System.Collections.Generic;

namespace NarrativeFrame {
    class Story{
        string title, author;
        StoryNode currentNode;
        List<StoryNode> nodes;
        int nodeIndex;
       
        public Story(){

        }
        public StoryNode AddNode(){
            if(nodes == null){
                nodes = new List<StoryNode>();
            }
            StoryNode newNode = new StoryNode();
            nodes.Add(newNode);
            return newNode;
        }
        public StoryNode GetNodeAtIndex(int i){
            if(i < nodes.Count){
                return nodes[i];
            }else{
                Console.WriteLine("GetNodeAtIndex in Story.cs has an issue.");
                return null;
            }
        }

        public void StartStory(){
            nodeIndex = 0;
            currentNode = nodes[0];
            Console.Write("The story begins...");
            Console.ReadLine();
            PrintCurrentNode();
        }
        public void PrintCurrentNode(){
            Console.Clear();
            Console.WriteLine(currentNode.GetText());
            List<StoryNode> links = currentNode.GetLinks();
            if(links == null){
                Console.Write("Your story has come to an end!");
                Console.ReadLine();
                return;
            }
            //Console.WriteLine("Linked with: " + links.Count);
            for(int i = 0; i < links.Count; i++){               
                Console.WriteLine (i + ": " + links[i].GetPrompt());
            }
            int possibleChoices = links.Count;
            Console.WriteLine("\nWhat is your choice?");
            Console.Write(">");
            string input = Console.ReadLine();
            int choice;
            if(int.TryParse(input, out choice)){
                if(choice < 0 || choice >= possibleChoices){
                    Console.WriteLine("Choice not available.");
                    RestartCurrentNode();
                }else{
                    AdvanceStory(choice);
                }
            }else{
                if(input.Equals("x")){
                    Console.WriteLine("Story Quit.");
                    Console.ReadLine();
                    return;
                }
                Console.WriteLine("Choice not available.");
                RestartCurrentNode();
            }
            //currentNode = nodes
        }
        public void AdvanceStory(int choice){
            currentNode = currentNode.GetLinks()[choice];
            if(currentNode.GetJumpPoint() != null){
                Console.WriteLine(currentNode.GetJumpPoint().GetText());

                currentNode = currentNode.GetJumpPoint();
            }
            //jump, replace currentNode with jumpNode
            PrintCurrentNode();
        }
        public void RestartCurrentNode(){
            Console.Clear();
            PrintCurrentNode();
        }
        public void SpewStory(){
            foreach(var node in nodes){
                Console.WriteLine(node.GetText());
            }
        }
        public int GetStoryLength(){
            return nodes.Count;
        }
    }
}