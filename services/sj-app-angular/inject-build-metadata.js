const yargs = require("yargs");
const replace = require('replace-in-file');

const yargsOptions = yargs
  .usage('Usage: -r <git-revision-sha>')
  .option('r', { alias: 'revision', description: 'SHA hash of the git revision being built', type: 'string', demandOption: true })
  .argv;

const replaceOptions = {
  files: 'src/environments/environment.prod.ts',
  from: /{REVISION_HASH}/g,
  to: yargsOptions.revision
}

replace.sync(replaceOptions);
