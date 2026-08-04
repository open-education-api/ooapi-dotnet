# Regenerates the SVGs in this folder from their .puml sources.
#
# Requires Java (any recent JDK/JRE) and a local plantuml.jar. Point $env:PLANTUML_JAR at one, or
# place a copy at docs/diagrams/plantuml.jar.
#
# Usage: .\render.ps1

$ErrorActionPreference = "Stop"
Set-Location $PSScriptRoot

if ($env:PLANTUML_JAR) {
    $jar = $env:PLANTUML_JAR
} elseif (Test-Path "plantuml.jar") {
    $jar = "plantuml.jar"
} else {
    Write-Error "No PlantUML found. Set `$env:PLANTUML_JAR to a plantuml.jar path, or place one at docs/diagrams/plantuml.jar. Download: https://plantuml.com/download"
    exit 1
}

java -jar $jar -tsvg (Get-ChildItem -Filter "*.puml" | Select-Object -ExpandProperty FullName)

Write-Host "Rendered: api-model.svg, database-schema.svg"
