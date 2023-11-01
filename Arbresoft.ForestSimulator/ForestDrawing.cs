using System.Collections.Concurrent;
using Arbresoft.ForestSimulator.Models;

namespace Arbresoft.ForestSimulator
{
    public class ForestDrawing : IDrawable
	{
        private List<ITree> trees;

		public ForestDrawing()
		{
            this.trees = new();
		}

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            trees = trees.Where(tree => tree.GetAge() < 100).ToList();
            foreach (var tree in trees)
            {
                DrawTree(canvas, tree);
            }
        }

        public void AddTree(ITree tree)
        {
            this.trees.Add(tree);
        }

        public void AddOneYear()
        {
            var treesToAdd = new ConcurrentBag<ITree>();
            Parallel.ForEach(trees, (tree) =>
            {
                tree.AddOneYear();
                if (tree.CanReproduce(trees))
                {
                    treesToAdd.Add(tree.Reproduce());
                }
            });
            trees.AddRange(treesToAdd);
        }

        private void DrawTree(ICanvas canvas, ITree tree)
        {
            canvas.FillColor = tree.GetColor();
            canvas.FillCircle((int)tree.Location.X, (int)tree.Location.Y, tree.GetRadius());
        }
    }
}
