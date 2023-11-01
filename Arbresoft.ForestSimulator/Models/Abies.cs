namespace Arbresoft.ForestSimulator.Models
{
    public class Abies : Tree
    {
        public Abies(Point point) : base(point)
        {

        }

        public override Color GetColor()
        {
            return Colors.CornflowerBlue;
        }

        public override int GetRadius()
        {
            return (int)(GetAge() * 0.1);
        }

        public override Tree Reproduce()
        {
            this.Reproduced = true;

            return new Abies(seedLocation);
        }
    }
}

