using WhisperSubs.Configuration;
using Microsoft.Extensions.Logging;

namespace WhisperSubs.Providers
{
    public static class SubtitleProviderFactory
    {
        /// <summary>
        /// Returns the appropriate <see cref="ISubtitleProvider"/> based on configuration.
        ///
        /// Priority:
        ///   1. RemoteWhisperApiUrl is set → SubgenProvider (subgen HTTP API)
        ///   2. WhisperModelPath is set    → WhisperProvider (local whisper-cli binary)
        ///   3. Neither set               → throws (caller should guard before calling)
        /// </summary>
        public static ISubtitleProvider Create(PluginConfiguration config, ILoggerFactory loggerFactory)
        {
            if (!string.IsNullOrWhiteSpace(config.RemoteWhisperApiUrl))
            {
                var logger = loggerFactory.CreateLogger<SubgenProvider>();
                return new SubgenProvider(
                    config.RemoteWhisperApiUrl,
                    config.RemoteWhisperApiKey,
                    logger);
            }

            // Fall back to local whisper-cli binary
            var localLogger = loggerFactory.CreateLogger<WhisperProvider>();
            return new WhisperProvider(
                config.WhisperBinaryPath,
                config.WhisperModelPath,
                config.WhisperThreadCount,
                localLogger);
        }
    }
}
