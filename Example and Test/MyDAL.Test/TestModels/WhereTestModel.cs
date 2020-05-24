﻿using MyDAL.Test.Enums;
using System;
using System.Collections.Generic;

namespace MyDAL.Test.Entities
{
    public class WhereTestModel
    {
        
        public DateTime CreatedOn { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
        
        public AgentLevel? AgentLevelNull { get; set; }
        
        public string ContainStr2 { get; set; }
        
        public AgentLevel?[] In_Array_枚举 { get; set; }
        
        public string[] In_Array_String { get; set; }
        
    }
}
