public class TheExplorersConfig
{

	/* PLAYER STATS ---------------------------------------------------------------------------------------- */

	public static int HEALTH_MAX = 5;
	public static int HEALTH_INCREMENT = 1;
	public static int HEALTH_STARTING = 5;

	public static int MINDLIGHT_MAX = 5;
	public static int MINDLIGHT_INCREMENT = 1;
	public static int MINDLIGHT_STARTING = 0;

	public static int SCORE_MAX = 9999;
	public static int SCORE_INCREMENT = 100;
	public static int SCORE_STARTING = 0;

	/* VOLUME ---------------------------------------------------------------------------------------- */

	public static float VOLUME_DEFAULT = 0.80f;
	public static float VOLUME_MAX = 1f;
	public static float VOLUME_MIN = 0f;

	/* SCENE INDEXES ---------------------------------------------------------------------------------------- */

//	public static int SCENE_SPLASH = 0;
	public static int SCENE_MAIN_MENU = 0;

}

/* ENUMs ---------------------------------------------------------------------------------------- */

public enum AUDIO_TYPE {
	SFX, BGM
}

public enum GROUND_SIDE {
	BOTTOM, TOP, SIDE
}

public enum WALL_SLIDE_SIDE {
	LEFT, RIGHT
}

/* NOTE: the values of this enum has to be present in the EditorGUI tags and vice versa. */
public enum OBJECT_TAG {

	Untagged,
	Floor,

	Player,
	Ally,

	Enemy,

	Item_Health

}