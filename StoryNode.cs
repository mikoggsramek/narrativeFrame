using System;
using System.Collections.Generic;

namespace NarrativeFrame {
    class StoryNode{
        
        string text;
        string prompt;
        List<StoryNode> links;
        StoryNode jumpPoint;
        int jumpRef;
        int level;
        public StoryNode(){
            text = "";
            level = 0;
        }
        public void AddLink(StoryNode newLink){
            if(links == null){
                links = new List<StoryNode>();
            }
            links.Add(newLink);
        }
        public void SetJumpPoint(StoryNode newJump){
            jumpPoint = newJump;
        }
        public StoryNode GetJumpPoint(){
            return jumpPoint;
        }
        public string GetText(){
            return text;
        }
        public List<StoryNode> GetLinks(){
            return links;
        }
        public void SetText(string text){
            this.text = text;
        }
        public string GetPrompt(){
            return prompt;
        }
        public void SetPrompt(string prompt){
            this.prompt = prompt;
        }
        public int GetLevel(){
            return level;
        }
        public void SetLevel(int level){
            this.level = level;
        }
        public void SetJumpRef(int refere){
            this.jumpRef = refere;
        }
        public int GetJumpRef(){
            return jumpRef;
        }
    }
}