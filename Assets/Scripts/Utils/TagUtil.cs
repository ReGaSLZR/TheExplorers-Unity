public class TagUtil {

	public static bool IsUntagged(string tag) {
		return tag.Equals(OBJECT_TAG.Untagged.ToString());
	}

	public static bool IsTagPlayer(string tag) {
		return tag.Equals(OBJECT_TAG.Player.ToString());
	}

	public static bool IsTagWalkable(string tag) {
		return tag.Equals(OBJECT_TAG.Floor.ToString());
	}

	public static bool IsTagSlideable(string tag) {
		return tag.Equals(OBJECT_TAG.Floor.ToString());
	}

}