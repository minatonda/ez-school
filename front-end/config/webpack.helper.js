const path = require("path");

// Helper functions
const ROOT = path.resolve(__dirname, "..");

function fromRootPath(args) {
  args = Array.prototype.slice.call(arguments, 0);
  return path.join.apply(path, [ROOT].concat(args));
}

exports.fromRootPath = fromRootPath;
