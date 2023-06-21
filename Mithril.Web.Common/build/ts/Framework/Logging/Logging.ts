// Log levels defined in order of severity (lowest to highest)
export type LogLevel = 'Verbose' | 'Debug' | 'Information' | 'Warning' | 'Error' | 'Fatal';

// Log event interface
export interface LogEvent {
    // The timestamp of the log event
    timestamp: Date;
    // The log level
    level: LogLevel;
    // The message
    message: string;
    // The exception
    exception?: Error;
    // The properties of the log event
    properties: { [key: string]: any };
}

// Log event enricher interface
// An enricher can add additional properties to a log event before it is written to a sink
export interface LogEventEnricher {
    // Enriches a log event
    // event: The log event to enrich
    enrich(event: LogEvent): void;
}

// Log filter interface
// A filter can be used to filter log events before they are written to a sink
export interface LogFilter {
    // Filters a log event and returns true if the event should be written to a sink or false if it should be discarded
    // event: The log event to filter
    // Returns true if the event should be written to a sink or false if it should be discarded
    filter(event: LogEvent): boolean;
}

// Log output formatter interface
export interface OutputFormatter {
    // Formats a log event into a string
    // event: The log event to format into a string
    // Returns the formatted log event
    format(event: LogEvent): string;
}

// Default log output formatter implementation
export class DefaultFormatter implements OutputFormatter {
    // Creates a new default log output formatter
    // outputFormat: The output format to use when formatting log events (defaults to "{Timestamp}: [{Level}]: {Message}{Exception}")
    // The following placeholders can be used in the output format:
    // {Timestamp}: The timestamp of the log event
    // {Level}: The log level of the log event
    // {Message}: The message of the log event
    // {Exception}: The exception of the log event
    // {PropertyName}: The properties of the log event (where PropertyName is the name of the property)
    constructor(outputFormat?: string) {
        this.outputFormat = outputFormat ?? "{Timestamp}: [{Level}]: {Message}{Exception}";
    }

    // The output format to use when formatting log events (defaults to "{Timestamp}: [{Level}]: {Message}{Exception}")
    private outputFormat: string;

    // Formats a log event into a string using the output format
    // event: The log event to format into a string
    // Returns the formatted log event
    public format(event: LogEvent): string {
        return this.outputFormat.replace(/{(\w+)}/g, (match, propertyName) => {
            if (propertyName === "Timestamp") {
                return event.timestamp.toISOString();
            } else if (propertyName === "Level") {
                return event.level;
            } else if (propertyName === "Message") {
                return event.message;
            } else if (propertyName === "Exception") {
                return event.exception ? "\n" + event.exception.stack : "";
            } else {
                return event.properties[propertyName];
            }
        });
    }
}

// Log filter implementation that filters log events by minimum level (lowest level to write)
export class MinimumLevelLogFilter implements LogFilter {
    // Creates a new minimum level log filter
    // minimumLevel: The minimum level to write (defaults to "Debug")
    constructor(minimumLevel: LogLevel = "Debug") {
        this.minimumLevel = minimumLevel;
        this.allowedLevels = this.allowedLevels.slice(this.allowedLevels.indexOf(minimumLevel));
    }

    // The allowed levels
    private allowedLevels: LogLevel[] = ["Verbose", "Debug", "Information", "Warning", "Error", "Fatal"];

    // Filters a log event and returns true if the event should be written to a sink or false if it should be discarded
    // event: The log event to filter
    // Returns true if the event should be written to a sink or false if it should be discarded
    public filter(event: LogEvent): boolean {
        return this.allowedLevels.indexOf(event.level) >= 0;
    }

    // The minimum level to write
    private minimumLevel: LogLevel;
}

// Console sink implementation that writes log events to the console
export class ConsoleSink implements LogSink {
    // Writes a log event to the console
    // event: The log event to write
    write(event: LogEvent): void {
        if (event.level === "Error" || event.level === "Fatal") {
            console.error(event.message, event.exception, event.properties || "");
        } else if (event.level === "Warning") {
            console.warn(event.message, event.properties || "");
        } else if (event.level === "Information") {
            console.info(event.message, event.properties || "");
        } else if (event.level === "Debug") {
            console.debug(event.message, event.properties || "");
        } else {
            console.log(event.message, event.properties || "");
        }
    }
}

// The pipline for a log sink that can be used to add filters, formatters and enrichers
// The pipeline is used to process log events before they are written to the sink
export class LogSinkPipeline {
    // Creates a new log sink pipeline
    // loggerConfiguration: The logger configuration that the pipeline belongs to
    constructor(loggerConfiguration: LoggerConfiguration) {
        this.loggerConfiguration = loggerConfiguration;
    }
    // The sink that the pipeline writes to
    private sink: LogSink;
    // The filters that the pipeline uses to filter log events
    private filters: LogFilter[] = [];
    // The formatter that the pipeline uses to format log events
    private formatter: OutputFormatter;
    // The enrichers that the pipeline uses to enrich log events
    private enrichers: LogEventEnricher[] = [];
    // The logger configuration that the pipeline belongs to
    private loggerConfiguration: LoggerConfiguration;

    // Sets the sink that the pipeline writes to
    // sink: The sink to write to
    // Returns the logger configuration that the pipeline belongs to
    public writeTo(sink: LogSink): LoggerConfiguration {
        this.sink = sink;
        return this.loggerConfiguration;
    }

    // Adds a filter to the pipeline that filters log events by minimum level (lowest level to write)
    // level: The minimum level to write
    // Returns the pipeline
    public minimumLevel(level: LogLevel): LogSinkPipeline {
        this.filters.push(new MinimumLevelLogFilter(level));
        return this;
    }

    // Adds a filter to the pipeline that filters log events before they are written to the sink
    // filter: The filter to add
    // Returns the pipeline
    public filter(filter: LogFilter): LogSinkPipeline {
        this.filters.push(filter);
        return this;
    }

    // Sets the formatter that the pipeline uses to format log events before they are written to the sink
    // formatter: The formatter to use
    // Returns the pipeline
    public formatUsing(formatter: OutputFormatter): LogSinkPipeline {
        this.formatter = formatter;
        return this;
    }

    // Adds an enricher to the pipeline that enriches log events before they are written to the sink
    // enricher: The enricher to add
    // Returns the pipeline
    public enrichWith(enricher: LogEventEnricher): LogSinkPipeline {
        this.enrichers.push(enricher);
        return this;
    }

    // Processes a log event by filtering, enriching and formatting it before writing it to the sink
    // event: The log event to process
    public process(event: LogEvent): void {
        this.formatter ??= new DefaultFormatter();
        this.sink ??= new ConsoleSink();
        let eventCopy: LogEvent = Object.assign({}, event) as LogEvent;
        if (!this.filters.some(filter => filter.filter(eventCopy))) {
            return;
        }
        this.enrichers.forEach(enricher => enricher.enrich(eventCopy));
        eventCopy.message = this.formatter.format(eventCopy) || eventCopy.message;
        this.sink.write(eventCopy);
    }
}

// Logger configuration that can be used to configure loggers
// The configuration is used to configure the sinks, filters, formatters and enrichers that are used by the logger
export class LoggerConfiguration {
    constructor() { }
    // The pipelines that the configuration uses to process log events
    private pipelines: LogSinkPipeline[] = [];

    // Sets the sink that the pipeline writes to
    // sink: The sink to write to
    // Returns the logger configuration that the pipeline belongs to
    public writeTo(sink: LogSink): LoggerConfiguration {
        let pipeline = new LogSinkPipeline(this);
        this.pipelines.push(pipeline);
        return pipeline.writeTo(sink);
    }

    // Adds a filter to the pipeline that filters log events by minimum level (lowest level to write)
    // level: The minimum level to write
    // Returns the pipeline
    public minimumLevel(level: LogLevel): LogSinkPipeline {
        let pipeline = new LogSinkPipeline(this);
        this.pipelines.push(pipeline);
        return pipeline.minimumLevel(level);
    }

    // Adds a filter to the pipeline that filters log events before they are written to the sink
    // filter: The filter to add
    // Returns the pipeline
    public filter(filter: LogFilter): LogSinkPipeline {
        let pipeline = new LogSinkPipeline(this);
        this.pipelines.push(pipeline);
        return pipeline.filter(filter);
    }

    // Sets the formatter that the pipeline uses to format log events before they are written to the sink
    // formatter: The formatter to use
    // Returns the pipeline
    public formatUsing(formatter: OutputFormatter): LogSinkPipeline {
        let pipeline = new LogSinkPipeline(this);
        this.pipelines.push(pipeline);
        return pipeline.formatUsing(formatter);
    }

    // Adds an enricher to the pipeline that enriches log events before they are written to the sink
    // enricher: The enricher to add
    // Returns the pipeline
    public enrichWith(enricher: LogEventEnricher): LogSinkPipeline {
        let pipeline = new LogSinkPipeline(this);
        this.pipelines.push(pipeline);
        return pipeline.enrichWith(enricher);
    }

    // Writes a log event to the configured sinks after processing it with the configured pipelines
    // level: The level of the log event
    // message: The message of the log event
    // properties: The properties of the log event
    // exception: The exception of the log event
    public write(level: LogLevel, message: string, properties?: { [key: string]: any }, exception?: Error): void {
        let currentEvent: LogEvent = {
            level: level,
            message: message,
            properties: properties,
            exception: exception,
            timestamp: new Date()
        };
        this.pipelines.forEach(pipeline => pipeline.process(currentEvent));
    }
}

// Log sink interface
export interface LogSink {
    // Writes a log event to the sink
    // event: The log event to write
    write(event: LogEvent): void;
}


// Logger class that is used to write log events
export class Logger {
    // Hides the constructor
    private constructor() { }
    // The logger configuration
    private static loggerConfiguration: LoggerConfiguration;

    // Gets the logger configuration that the logger uses to configure its sinks, filters, formatters and enrichers
    public static configure(): LoggerConfiguration {
        this.loggerConfiguration ??= window.LoggerConfiguration || new LoggerConfiguration();
        window.LoggerConfiguration = this.loggerConfiguration;
        return this.loggerConfiguration;
    }

    // Writes a log event to the logger
    // level: The level of the log event
    // message: The message of the log event
    // properties: The properties of the log event
    // exception: The exception of the log event
    public static write(level: LogLevel, message: string, properties?: { [key: string]: any }, exception?: Error): void {
        Logger.configure().write(level, message, properties, exception);
    }

    // Writes a log event to the logger with the Verbose level
    // message: The message of the log event
    // properties: The properties of the log event
    public static verbose(message: string, properties?: { [key: string]: any }): void {
        this.write("Verbose", message, properties);
    }

    // Writes a log event to the logger with the Debug level
    // message: The message of the log event
    // properties: The properties of the log event
    public static debug(message: string, properties?: { [key: string]: any }): void {
        this.write("Debug", message, properties);
    }

    // Writes a log event to the logger with the Information level
    // message: The message of the log event
    // properties: The properties of the log event
    public static information(message: string, properties?: { [key: string]: any }): void {
        this.write("Information", message, properties);
    }

    // Writes a log event to the logger with the Warning level
    // message: The message of the log event
    // properties: The properties of the log event
    public static warning(message: string, properties?: { [key: string]: any }): void {
        this.write("Warning", message, properties);
    }

    // Writes a log event to the logger with the Error level
    // message: The message of the log event
    // properties: The properties of the log event
    // exception: The exception of the log event
    public static error(message: string, properties?: { [key: string]: any }, exception?: Error): void {
        this.write("Error", message, properties, exception);
    }

    // Writes a log event to the logger with the Fatal level
    // message: The message of the log event
    // properties: The properties of the log event
    // exception: The exception of the log event
    public static fatal(message: string, properties?: { [key: string]: any }, exception?: Error): void {
        this.write("Fatal", message, properties, exception);
    }
}