#!/usr/bin/env bash
# Regenerates the SVGs in this folder from their .puml sources.
#
# Requires Java (any recent JDK/JRE) and a local plantuml.jar. Point PLANTUML_JAR at one, or
# install PlantUML via your package manager (e.g. `brew install plantuml` / `apt install
# plantuml`) and this script will just invoke `plantuml` directly if PLANTUML_JAR isn't set and
# no jar is found alongside this script.
#
# Usage: ./render.sh

set -euo pipefail
cd "$(dirname "$0")"

if [ -n "${PLANTUML_JAR:-}" ]; then
    java -jar "$PLANTUML_JAR" -tsvg ./*.puml
elif [ -f plantuml.jar ]; then
    java -jar plantuml.jar -tsvg ./*.puml
elif command -v plantuml >/dev/null 2>&1; then
    plantuml -tsvg ./*.puml
else
    echo "No PlantUML found. Set PLANTUML_JAR to a plantuml.jar path, place one at" >&2
    echo "docs/diagrams/plantuml.jar, or install the 'plantuml' command." >&2
    echo "Download: https://plantuml.com/download" >&2
    exit 1
fi

echo "Rendered: api-model.svg, database-schema.svg"
