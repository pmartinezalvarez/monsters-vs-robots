using UnityEngine;
using System.Collections;

public class MainConstants : MonoBehaviour
{
		public static int MAZINGER_MODEL_ID = 73;
		public static int GODZILLA_MODEL_ID = 72;
		public static string ABILITIES_TOYX_ATTR = "abilities";
		public static string ABILITIES_FIELD_DELIMITER = ":";
		public static string HEALTH_TOYX_ATTR = "health";
		public static string HEIGHT_TOYX_ATTR = "height";
		public static string STRENGTH_TOYX_ATTR = "strength";
		public static string WEIGHT_TOYX_ATTR = "weight";
		public static string NAME_SUFFIX_TOYX_ATTR = "name";
		public static string VALUE_SUFFIX_TOYX_ATTR = "value";
		public static string COUNT_SUFFIX_TOYX_ATTR = "count";
		public static float MESSAGES_TIME = 2.5F;
		public static float GAME_END_TIME = 10F;
		// GAME OBJECT NAMES
		public static string P1_GAMEOBJ = "Player1";
		public static string P2_GAMEOBJ = "Player2";
		public static string GODZILLA_SPLASH_SPRITERENDERER_GAMEOBJ = "GodzillaSplashSpriteRenderer";
		public static string MAZINGER_SPLASH_SPRITERENDERER_GAMEOBJ = "MazingerSplashSpriteRenderer";
		public static string LOADING_SPRITERENDERER_GAMEOBJ = "LoadingSpriteRenderer";
		public static string TWINSPRITE_FORM_GAMEOBJ = "TwinspriteForm";
		public static string TITLE_SPRITERENDERER_GAMEOBJ = "TitleSpriteRenderer";
		public static string DISPLAY_GUITEXT_GAMEOBJ = "DisplayGUIText";
		public static string PLAYER1_HEALTH_GUITEXT_GAMEOBJ = "Player1/HealthGUIText";
		public static string PLAYER1_GUITEXT_GAMEOBJ = "Player1/PlayerGUIText";
		public static string PLAYER1_IMAGE_SPRITERENDERER_GAMEOBJ = "Player1/ImageSpriteRenderer";
		public static string PLAYER1_NAMEBOX_SPRITERENDERER_GAMEOBJ = "Player1/NameBoxSpriteRenderer";
		public static string PLAYER2_HEALTH_GUITEXT_GAMEOBJ = "Player2/HealthGUIText";
		public static string PLAYER2_GUITEXT_GAMEOBJ = "Player2/PlayerGUIText";
		public static string PLAYER2_IMAGE_SPRITERENDERER_GAMEOBJ = "Player2/ImageSpriteRenderer";
		public static string PLAYER2_NAMEBOX_SPRITERENDERER_GAMEOBJ = "Player2/NameBoxSpriteRenderer";
		// RESOURCES
		public static string SPRITE_MAZINGER = "Sprites/mazinger";
		public static string SPRITE_MAZINGER_NAME_BOX_ACTIVE = "Sprites/mazinger-name-container-active";
		public static string SPRITE_MAZINGER_NAME_BOX = "Sprites/mazinger-name-container";
		public static string SPRITE_GODZILLA = "Sprites/godzilla";
		public static string SPRITE_GODZILLA_NAME_BOX_ACTIVE = "Sprites/godzilla-name-container-active";
		public static string SPRITE_GODZILLA_NAME_BOX = "Sprites/godzilla-name-container";
		public static string AUDIO_GODZILLA_DAMAGE = "Audio/godzilla-damage";
		public static string AUDIO_MAZINGER_DAMAGE = "Audio/mazinger-damage";
}
