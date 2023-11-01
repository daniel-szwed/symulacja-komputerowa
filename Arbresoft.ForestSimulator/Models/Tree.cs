namespace Arbresoft.ForestSimulator.Models
{
    public abstract class Tree : ITree
    {
        protected Point seedLocation;
        private int age = 0;

        public Point Location { get; }
        public bool Reproduced { get; set; }

        public Tree(Point point)
        {
            Location = point;
        }

        public void AddOneYear()
        {
            this.age += 1;
        }

        public int GetAge()
        {
            return this.age;
        }

        public virtual Color GetColor()
        {
            throw new NotImplementedException();
        }

        public virtual int GetRadius()
        {
            throw new NotImplementedException();
        }

        public Point GetSeedLocation(Point center, double radius)
        {
            Random rand = new Random();
            double randomX, randomY;

            do
            {
                randomX = center.X - radius + 2 * radius * rand.NextDouble();
                randomY = center.Y - radius + 2 * radius * rand.NextDouble();
            } while (Math.Pow(randomX - center.X, 2) + Math.Pow(randomY - center.Y, 2) > radius * radius);

            return new Point(randomX, randomY);
        }

        public bool IsLocationTaken(Point newLocation, double radius, List<ITree> trees)
        {
            bool isLocationTaken = false;

            Parallel.ForEach(trees, (tree, state) =>
            {
                double distanceSquared = (newLocation.X - tree.Location.X) * (newLocation.X - tree.Location.X)
                    + (newLocation.Y - tree.Location.Y) * (newLocation.Y - tree.Location.Y);
                double radiusSquared = radius * radius;

                if (distanceSquared < radiusSquared)
                {
                    isLocationTaken = true;
                    state.Stop(); // Early exit
                }
            });

            return isLocationTaken;
        }

        public bool CanReproduce(List<ITree> allTrees)
        {
            if (GetAge() < 50)
            {
                return false;
            }

            var treesWithoutCurrent = allTrees
                .Where(tree => tree.Location != this.Location)
                .Where(tree => !tree.Reproduced)
                .ToList();

            this.seedLocation = GetSeedLocation(this.Location, 20);
            var isLocationTaken = IsLocationTaken(seedLocation, 10, allTrees);

            if (isLocationTaken)
            {
                return false;
            }

            return true;
        }

        public virtual Tree Reproduce()
        {
            throw new NotImplementedException();
        }
    }
}
