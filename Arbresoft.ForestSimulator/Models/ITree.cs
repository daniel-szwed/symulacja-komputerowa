namespace Arbresoft.ForestSimulator.Models
{
    public interface ITree
    {
        Point Location { get; }
        bool Reproduced { get; set; }
        void AddOneYear();
        int GetAge();
        Color GetColor();
        int GetRadius();
        bool CanReproduce(List<ITree> allTrees);
        Tree Reproduce();
    }
}

