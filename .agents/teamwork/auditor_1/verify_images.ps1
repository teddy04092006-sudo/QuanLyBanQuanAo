# Determine project root relative to script location
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$projectRoot = (Get-Item "$scriptDir\..\..\..").FullName

Write-Output "Project Root: $projectRoot"

$srcDir = Join-Path $projectRoot "Images\QuanAo"
$binDir = Join-Path $projectRoot "bin\Debug\Images\QuanAo"

$srcFiles = Get-ChildItem -LiteralPath $srcDir -Filter 'QA*.jpg'
$binFiles = Get-ChildItem -LiteralPath $binDir -Filter 'QA*.jpg'

Write-Output ("Src Count: " + $srcFiles.Count)
Write-Output ("Bin Count: " + $binFiles.Count)

# Check all 200 QA001..QA200 exist
$missing = @()
1..200 | ForEach-Object {
    $name = ('QA{0:D3}.jpg' -f $_)
    if (-not (Test-Path (Join-Path $srcDir $name))) { $missing += $name }
}
if ($missing.Count -eq 0) {
    Write-Output "Missing QA001..QA200 in Src: NONE (All 200 present)"
} else {
    Write-Output ("Missing QA001..QA200 in Src: " + ($missing -join ', '))
}

# Compute SHA256 hashes
$hashes = $srcFiles | ForEach-Object { (Get-FileHash -LiteralPath $_.FullName -Algorithm SHA256).Hash }
$uniqueHashes = $hashes | Select-Object -Unique
Write-Output ("Total hashes: " + $hashes.Count + " | Unique hashes: " + $uniqueHashes.Count)

# File size stats
$sizes = $srcFiles | ForEach-Object { $_.Length }
$minSize = ($sizes | Measure-Object -Minimum).Minimum
$maxSize = ($sizes | Measure-Object -Maximum).Maximum
$avgSize = ($sizes | Measure-Object -Average).Average
Write-Output ("File sizes -> Min: $minSize bytes, Max: $maxSize bytes, Avg: " + [Math]::Round($avgSize, 2) + " bytes")

# Check dimensions and formats for all 200 images
Add-Type -AssemblyName System.Drawing
$badDim = @()
foreach ($f in $srcFiles) {
    $img = [System.Drawing.Image]::FromFile($f.FullName)
    if ($img.Width -ne 500 -or $img.Height -ne 600) {
        $badDim += ($f.Name + ': ' + $img.Width + 'x' + $img.Height)
    }
    $img.Dispose()
}
if ($badDim.Count -eq 0) {
    Write-Output "Dimensions Check: PASS (All 200 images are exactly 500x600)"
} else {
    Write-Output ("Dimensions Check: FAIL (" + ($badDim -join ', ') + ")")
}
