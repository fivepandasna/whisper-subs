using System.Text.Json.Serialization;
using MediaBrowser.Model.Plugins;

namespace WhisperSubs.Configuration
{
    public class PluginConfiguration : BasePluginConfiguration
    {
        public string WhisperModelPath { get; set; } = "";
        public string WhisperBinaryPath { get; set; } = "";
        public bool EnableAutoGeneration { get; set; } = false;

        /// <summary>
        /// Default language for subtitle generation.
        /// "auto" = detect from audio stream metadata, fall back to whisper auto-detection.
        /// Any ISO 639-1 code (e.g. "es", "en", "fr") forces that language.
        /// </summary>
        public string DefaultLanguage { get; set; } = "auto";

        /// <summary>
        /// Controls whether to generate full subtitles, forced-only subtitles, or both.
        /// </summary>
        [JsonConverter(typeof(SubtitleModeConverter))]
        public SubtitleMode SubtitleMode { get; set; } = SubtitleMode.Full;

        /// <summary>
        /// When enabled, music libraries are scanned and audio tracks receive
        /// .lrc lyrics files generated via whisper transcription.
        /// Experimental: whisper models are optimized for speech, not singing.
        /// </summary>
        public bool EnableLyricsGeneration { get; set; } = false;

        /// <summary>
        /// When enabled, generates English subtitles via whisper's translate task
        /// for media that lacks an English audio track.
        /// Only applies when SubtitleMode includes Full subtitles.
        /// </summary>
        public bool EnableTranslation { get; set; } = false;

        /// <summary>
        /// Number of threads for local whisper.cpp inference. 0 = whisper default (4).
        /// Not used when RemoteWhisperApiUrl is set.
        /// </summary>
        public int WhisperThreadCount { get; set; } = 0;

        /// <summary>
        /// Base URL of your subgen instance.
        /// Example: http://192.168.1.100:8000
        ///
        /// subgen exposes:
        ///   POST /asr              – transcription / translation
        ///   POST /detect-language  – language detection
        /// </summary>
        public string RemoteWhisperApiUrl { get; set; } = "";

        /// <summary>
        /// Not used by subgen (subgen selects its model via its own configuration).
        /// Kept for UI display purposes only.
        /// </summary>
        public string RemoteWhisperModel { get; set; } = "";

        /// <summary>
        /// Optional Bearer token if subgen is placed behind an authenticating reverse proxy.
        /// Leave empty for a standard local subgen deployment.
        /// </summary>
        public string RemoteWhisperApiKey { get; set; } = "";

        /// <summary>
        /// When enabled, subtitle generation pauses while any user is actively
        /// playing media and resumes automatically when playback stops.
        /// </summary>
        public bool PauseOnPlayback { get; set; } = false;

        public List<string> EnabledLibraries { get; set; } = new List<string>();

        /// <summary>
        /// Optional webhook URL to call when the scheduled task completes.
        /// </summary>
        public string TaskCompletionWebhookUrl { get; set; } = "";

        public PluginConfiguration()
        {
        }
    }
}
