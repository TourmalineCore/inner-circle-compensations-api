function fn() {
    return {
        getEnvVariable: function (variable) {
            var System = Java.type('java.lang.System');

            return System.getenv(variable);
        },

        getAuthHeaders: function (tokenValue) {
            return {
                [this.getAuthHeaderKey()]: this.getAuthHeaderValue(tokenValue)
            }
        },

        getAuthHeaderValue: function (tokenValue) {
            return this.shouldUseFakeExternalDependencies()
                ? tokenValue
                : 'Bearer ' + tokenValue;
        },

        getAuthHeaderKey: function () {
            return this.shouldUseFakeExternalDependencies()
                ? 'X-DEBUG-TOKEN'
                : 'Authorization';
        },

        shouldUseFakeExternalDependencies: function () {
            var System = Java.type('java.lang.System');

            return System.getenv('SHOULD_USE_FAKE_EXTERNAL_DEPENDENCIES') === 'true';
        },

        command: function (line) {
            var proc = karate.fork({ redirectErrorStream: false, line: line });

            // function listener(line) {
            //     if (line.contains('file sent')) {
            //       karate.log('stopping tool');      
            //       karate.signal(proc.sysOut);
            //     }
            // }

            // karate.fork(line);
            // proc.waitSync();
            karate.set('sysOut', proc.sysOut);
            karate.set('sysErr', proc.sysErr);
            // karate.set('exitCode', proc.exitCode);
        },
    }
}
