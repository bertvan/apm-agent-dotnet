// Licensed to Elasticsearch B.V under
// one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Elastic.Apm.Config;
using Elastic.Apm.Helpers;
using Elastic.Apm.Logging;
using static Elastic.Apm.Config.ConfigConsts;

namespace Elastic.Apm.Tests.Utilities
{
	public class MockConfiguration : AbstractConfigurationReader, IConfiguration, IConfigurationSnapshotDescription
	{
		public const string Origin = "unit test configuration";
		private const string ThisClassName = nameof(MockConfiguration);
		private readonly string _apiKey;
		private readonly string _applicationNamespaces;
		private readonly string _captureBody;
		private readonly string _captureBodyContentTypes;
		private readonly string _captureHeaders;
		private readonly string _centralConfig;
		private readonly string _cloudProvider;
		private readonly string _description;
		private readonly string _disableMetrics;
		private readonly string _enabled;
		private readonly string _enableOpenTelemetryBridge;
		private readonly string _environment;
		private readonly string _excludedNamespaces;
		private readonly string _exitSpanMinDuration;
		private readonly string _flushInterval;
		private readonly string _globalLabels;
		private readonly string _hostName;
		private readonly string _ignoreMessageQueues;
		private readonly string _logLevel;
		private readonly string _maxBatchEventCount;
		private readonly string _maxQueueEventCount;
		private readonly string _metricsInterval;
		private readonly string _recording;
		private readonly string _sanitizeFieldNames;
		private readonly string _secretToken;
		private readonly string _serverCert;
		private readonly string _serverUrl;
		private readonly string _serverUrls;
		private readonly string _useWindowsCredentials;
		private readonly string _serviceName;
		private readonly string _serviceNodeName;
		private readonly string _serviceVersion;
		private readonly string _spanCompressionEnabled;
		private readonly string _spanCompressionExactMatchMaxDuration;
		private readonly string _spanCompressionSameKindMaxDuration;
		private readonly string _spanStackTraceMinDurationInMilliseconds;
		private readonly string _spanFramesMinDurationInMilliseconds;
		private readonly string _stackTraceLimit;
		private readonly string _traceContextIgnoreSampledFalse;
		private readonly string _traceContinuationStrategy;
		private readonly string _transactionIgnoreUrls;
		private readonly string _transactionMaxSpans;
		private readonly string _transactionSampleRate;
		private readonly string _useElasticTraceparentHeader;
		private readonly string _verifyServerCert;

		public MockConfiguration(IApmLogger logger = null,
			string logLevel = null,
			string serverUrls = null,
			string serviceName = null,
			string serviceVersion = null,
			string environment = null,
			string serviceNodeName = null,
			string secretToken = null,
			string apiKey = null,
			string captureHeaders = null,
			string centralConfig = null,
			string description = null,
			string enableOpenTelemetryBridge = null,
			string exitSpanMinDuration = null,
			string transactionSampleRate = null,
			string transactionMaxSpans = null,
			string metricsInterval = null,
			string captureBody = SupportedValues.CaptureBodyOff,
			string stackTraceLimit = null,
			string spanStackTraceMinDurationInMilliseconds = null,
			string spanFramesMinDurationInMilliseconds = null,
			string captureBodyContentTypes = DefaultValues.CaptureBodyContentTypes,
			string flushInterval = null,
			string maxBatchEventCount = null,
			string maxQueueEventCount = null,
			string sanitizeFieldNames = null,
			string globalLabels = null,
			string disableMetrics = null,
			string verifyServerCert = null,
			string useElasticTraceparentHeader = null,
			string applicationNamespaces = null,
			string excludedNamespaces = null,
			string transactionIgnoreUrls = null,
			string hostName = null,
			// none is **not** the default value, but we don't want to query for cloud metadata in all tests
			string cloudProvider = SupportedValues.CloudProviderNone,
			string enabled = null,
			string recording = null,
			string serverUrl = null,
			string useWindowsCredentials = null,
			string serverCert = null,
			string ignoreMessageQueues = null,
			string traceContextIgnoreSampledFalse = null,
			string spanCompressionEnabled = null,
			string spanCompressionExactMatchMaxDuration = null,
			string spanCompressionSameKindMaxDuration = null,
			string traceContinuationStrategy = null
		) : base(logger, ThisClassName)
		{
			_serverUrls = serverUrls;
			_logLevel = logLevel;
			_serviceName = serviceName;
			_serviceVersion = serviceVersion;
			_environment = environment;
			_serviceNodeName = serviceNodeName;
			_secretToken = secretToken;
			_apiKey = apiKey;
			_captureHeaders = captureHeaders;
			_centralConfig = centralConfig;
			_description = description;
			_enableOpenTelemetryBridge = enableOpenTelemetryBridge;
			_transactionSampleRate = transactionSampleRate;
			_transactionMaxSpans = transactionMaxSpans;
			_metricsInterval = metricsInterval;
			_captureBody = captureBody;
			_stackTraceLimit = stackTraceLimit;
			_spanStackTraceMinDurationInMilliseconds = spanStackTraceMinDurationInMilliseconds;
			_spanFramesMinDurationInMilliseconds = spanFramesMinDurationInMilliseconds;
			_captureBodyContentTypes = captureBodyContentTypes;
			_flushInterval = flushInterval;
			_maxBatchEventCount = maxBatchEventCount;
			_maxQueueEventCount = maxQueueEventCount;
			_sanitizeFieldNames = sanitizeFieldNames;
			_globalLabels = globalLabels;
			_disableMetrics = disableMetrics;
			_verifyServerCert = verifyServerCert;
			_useElasticTraceparentHeader = useElasticTraceparentHeader;
			_applicationNamespaces = applicationNamespaces;
			_excludedNamespaces = excludedNamespaces;
			_transactionIgnoreUrls = transactionIgnoreUrls;
			_hostName = hostName;
			_cloudProvider = cloudProvider;
			_enabled = enabled;
			_recording = recording;
			_serverUrl = serverUrl;
			_useWindowsCredentials = useWindowsCredentials;
			_serverCert = serverCert;
			_ignoreMessageQueues = ignoreMessageQueues;
			_traceContextIgnoreSampledFalse = traceContextIgnoreSampledFalse;
			_spanCompressionEnabled = spanCompressionEnabled;
			_spanCompressionExactMatchMaxDuration = spanCompressionExactMatchMaxDuration;
			_spanCompressionSameKindMaxDuration = spanCompressionSameKindMaxDuration;
			_exitSpanMinDuration = exitSpanMinDuration;
			_traceContinuationStrategy = traceContinuationStrategy;
		}

		public string ApiKey => ParseApiKey(Kv(EnvVarNames.ApiKey, _apiKey, Origin));

		public IReadOnlyCollection<string> ApplicationNamespaces =>
			ParseApplicationNamespaces(new ConfigurationKeyValue(EnvVarNames.ApplicationNamespaces, _applicationNamespaces, Origin));

		public string CaptureBody => ParseCaptureBody(Kv(EnvVarNames.CaptureBody, _captureBody, Origin));

		public List<string> CaptureBodyContentTypes =>
			ParseCaptureBodyContentTypes(Kv(EnvVarNames.CaptureBodyContentTypes, _captureBodyContentTypes, Origin));

		public bool CaptureHeaders => ParseCaptureHeaders(Kv(EnvVarNames.CaptureHeaders, _captureHeaders, Origin));
		public bool CentralConfig => ParseCentralConfig(Kv(EnvVarNames.CentralConfig, _centralConfig, Origin));
		public string CloudProvider => ParseCloudProvider(Kv(EnvVarNames.CloudProvider, _cloudProvider, Origin));

		public string Description => _description ?? nameof(MockConfiguration);

		public IReadOnlyList<WildcardMatcher> DisableMetrics =>
			ParseDisableMetrics(Kv(EnvVarNames.DisableMetrics, _disableMetrics, Origin));

		public bool Enabled => ParseEnabled(Kv(EnvVarNames.Enabled, _enabled, Origin));

		public bool EnableOpenTelemetryBridge =>
			ParseEnableOpenTelemetryBridge(Kv(EnvVarNames.EnableOpenTelemetryBridge, _enableOpenTelemetryBridge, Origin));

		public string Environment => ParseEnvironment(Kv(EnvVarNames.Environment, _environment, Origin));

		public IReadOnlyCollection<string> ExcludedNamespaces =>
			ParseExcludedNamespaces(new ConfigurationKeyValue(EnvVarNames.ExcludedNamespaces, _excludedNamespaces, Origin));

		public double ExitSpanMinDuration => ParseExitSpanMinDuration(Kv(EnvVarNames.ExitSpanMinDuration, _exitSpanMinDuration, Origin));

		public TimeSpan FlushInterval => ParseFlushInterval(Kv(EnvVarNames.FlushInterval, _flushInterval, Origin));

		public IReadOnlyDictionary<string, string> GlobalLabels =>
			ParseGlobalLabels(Kv(EnvVarNames.GlobalLabels, _globalLabels, Origin));

		public string HostName => ParseHostName(Kv(EnvVarNames.HostName, _hostName, Origin));

		public IReadOnlyList<WildcardMatcher> IgnoreMessageQueues =>
			ParseIgnoreMessageQueues(Kv(EnvVarNames.IgnoreMessageQueues, _ignoreMessageQueues, Origin));

		public LogLevel LogLevel => ParseLogLevel(Kv(EnvVarNames.LogLevel, _logLevel, Origin));
		public int MaxBatchEventCount => ParseMaxBatchEventCount(Kv(EnvVarNames.MaxBatchEventCount, _maxBatchEventCount, Origin));
		public int MaxQueueEventCount => ParseMaxQueueEventCount(Kv(EnvVarNames.MaxQueueEventCount, _maxQueueEventCount, Origin));
		public double MetricsIntervalInMilliseconds => ParseMetricsInterval(Kv(EnvVarNames.MetricsInterval, _metricsInterval, Origin));
		public bool Recording => ParseRecording(Kv(KeyNames.Recording, _recording, Origin));

		public IReadOnlyList<WildcardMatcher> SanitizeFieldNames =>
			ParseSanitizeFieldNames(Kv(EnvVarNames.SanitizeFieldNames, _sanitizeFieldNames, Origin));

		public string SecretToken => ParseSecretToken(Kv(EnvVarNames.SecretToken, _secretToken, Origin));

		public string ServerCert => ParseServerCert(Kv(EnvVarNames.ServerCert, _serverCert, Origin));

		public Uri ServerUrl
		{
			get
			{
				return _serverUrl != null
					? ParseServerUrl(Kv(EnvVarNames.ServerUrl, _serverUrl, Origin))
#pragma warning disable 618
					: ServerUrls[0];
#pragma warning restore 618
			}
		}

		[Obsolete("Use ServerUrl")]
		public IReadOnlyList<Uri> ServerUrls => ParseServerUrls(_serverUrls != null
			? Kv(EnvVarNames.ServerUrls, _serverUrls, Origin)
			: Kv(EnvVarNames.ServerUrl, _serverUrl, Origin));

		public bool UseWindowsCredentials => ParseUseWindowsCredentials(Kv(EnvVarNames.UseWindowsCredentials, _useWindowsCredentials, Origin));
		public string ServiceName => ParseServiceName(Kv(EnvVarNames.ServiceName, _serviceName, Origin));
		public string ServiceNodeName => ParseServiceNodeName(Kv(EnvVarNames.ServiceNodeName, _serviceNodeName, Origin));
		public string ServiceVersion => ParseServiceVersion(Kv(EnvVarNames.ServiceVersion, _serviceVersion, Origin));
		public bool SpanCompressionEnabled => ParseSpanCompressionEnabled(Kv(EnvVarNames.SpanCompressionEnabled, _spanCompressionEnabled, Origin));

		public double SpanCompressionExactMatchMaxDuration =>
			ParseSpanCompressionExactMatchMaxDuration(Kv(EnvVarNames.SpanCompressionExactMatchMaxDuration, _spanCompressionExactMatchMaxDuration,
				Origin));

		public double SpanCompressionSameKindMaxDuration => ParseSpanCompressionSameKindMaxDuration(Kv(EnvVarNames.SpanCompressionSameKindMaxDuration,
			_spanCompressionSameKindMaxDuration, Origin));

		public double SpanStackTraceMinDurationInMilliseconds => ParseSpanStackTraceMinDurationInMilliseconds(Kv(
			EnvVarNames.SpanStackTraceMinDuration,
			_spanStackTraceMinDurationInMilliseconds, Origin));

		[Obsolete("Use SpanStackTraceMinDurationInMilliseconds")]
		public double SpanFramesMinDurationInMilliseconds => ParseSpanFramesMinDurationInMilliseconds(Kv(
			EnvVarNames.SpanFramesMinDuration,
			_spanFramesMinDurationInMilliseconds, Origin));

		public int StackTraceLimit => ParseStackTraceLimit(Kv(EnvVarNames.StackTraceLimit, _stackTraceLimit, Origin));

		public bool TraceContextIgnoreSampledFalse =>
			ParseTraceContextIgnoreSampledFalse(Kv(EnvVarNames.TraceContextIgnoreSampledFalse, _traceContextIgnoreSampledFalse, Origin));

		public string TraceContinuationStrategy =>
			ParseTraceContinuationStrategy(Kv(EnvVarNames.TraceContinuationStrategy, _traceContinuationStrategy, Origin));

		public IReadOnlyList<WildcardMatcher> TransactionIgnoreUrls =>
			ParseTransactionIgnoreUrls(Kv(EnvVarNames.TransactionIgnoreUrls, _transactionIgnoreUrls, Origin));

		public int TransactionMaxSpans => ParseTransactionMaxSpans(Kv(EnvVarNames.TransactionMaxSpans, _transactionMaxSpans, Origin));

		public double TransactionSampleRate =>
			ParseTransactionSampleRate(Kv(EnvVarNames.TransactionSampleRate, _transactionSampleRate, Origin));

		public bool UseElasticTraceparentHeader =>
			ParseUseElasticTraceparentHeader(Kv(EnvVarNames.UseElasticTraceparentHeader, _useElasticTraceparentHeader, Origin));

		public bool VerifyServerCert =>
			ParseVerifyServerCert(Kv(EnvVarNames.VerifyServerCert, _verifyServerCert, Origin));
	}
}
