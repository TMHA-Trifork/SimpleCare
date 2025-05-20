import {
  BatchSpanProcessor,
  WebTracerProvider,
} from "@opentelemetry/sdk-trace-web";
import { OTLPTraceExporter } from "@opentelemetry/exporter-trace-otlp-proto";
import { registerInstrumentations } from "@opentelemetry/instrumentation";
import { FetchInstrumentation } from "@opentelemetry/instrumentation-fetch";
import { ZoneContextManager } from "@opentelemetry/context-zone";
import { resourceFromAttributes } from "@opentelemetry/resources";
import { W3CTraceContextPropagator } from "@opentelemetry/core";
import { propagation } from "@opentelemetry/api";

// Create a custom resource with the service name
const resource = resourceFromAttributes({
  "service.name": "Frontend",
});

const exporter = new OTLPTraceExporter({
  url: "http://localhost:18890/v1/traces",
  headers: {
    "Content-Type": "application/json",
  },
});

const provider = new WebTracerProvider({
  resource: resource,
  spanProcessors: [new BatchSpanProcessor(exporter)],
});

// Register W3C Trace Context Propagator
propagation.setGlobalPropagator(new W3CTraceContextPropagator());

// Register the provider and use Zone context manager
provider.register({
  contextManager: new ZoneContextManager(),
});

registerInstrumentations({
  instrumentations: [
    new FetchInstrumentation({
      clearTimingResources: true,
      propagateTraceHeaderCorsUrls: [
        /^http:\/\/localhost:\d+/,
        /^https:\/\/localhost:\d+/,
      ],
    }),
  ],
});

console.info("OpenTelemetry tracing initialized");
