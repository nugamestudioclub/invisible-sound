using Godot;

namespace Invisiblesound.Scripts {
	public class Walkmesh : Polygon2D {
		[field: Export]
		public WalkmeshMaterial MaterialType { get; private set; }
	}
}