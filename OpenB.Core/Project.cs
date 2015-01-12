namespace OpenB.Core
{
    public class Project
    {
        public string Name { get; private set; }

        public Project(string name)
        {
            Name = name;
        }
    }
}