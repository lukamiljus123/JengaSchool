using System;
using System.Collections.Generic;

[Serializable]
public struct Block
{
    public int id;
    public string subject;
    public string grade;
    public Mastery mastery;
    public string domainid;
    public string domain;
    public string cluster;
    public string standardid;
    public string standarddescription;
}

[Serializable]
public struct Blocks
{
    public List<Block> blocks;
}