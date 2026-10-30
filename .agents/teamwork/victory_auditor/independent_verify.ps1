[Console]::OutputEncoding = [System.Text.Encoding]::UTF8
Add-Type -AssemblyName System.Drawing

$projectRoot = Split-Path (Split-Path (Split-Path $PSScriptRoot -Parent) -Parent) -Parent
$srcDir = Join-Path $projectRoot "Images\QuanAo"
$binDir = Join-Path $projectRoot "bin\Debug\Images\QuanAo"

Write-Output "Resolved Project Root: $projectRoot"
Write-Output "Source Dir Exists: $(Test-Path $srcDir)"
Write-Output "Bin Dir Exists: $(Test-Path $binDir)"

Write-Output "`n=== 1. Checking File Existence & Count ==="
$srcFiles = Get-ChildItem -Path $srcDir -Filter "QA*.jpg"
$binFiles = Get-ChildItem -Path $binDir -Filter "QA*.jpg"

Write-Output "Source QA files count: $($srcFiles.Count)"
Write-Output "Bin QA files count: $($binFiles.Count)"

$missingSrc = @()
$missingBin = @()
for ($i = 1; $i -le 200; $i++) {
    $name = ("QA{0:D3}.jpg" -f $i)
    if (-not (Test-Path (Join-Path $srcDir $name))) { $missingSrc += $name }
    if (-not (Test-Path (Join-Path $binDir $name))) { $missingBin += $name }
}

Write-Output "Missing in Source: $($missingSrc.Count) -> $($missingSrc -join ', ')"
Write-Output "Missing in Bin: $($missingBin.Count) -> $($missingBin -join ', ')"

Write-Output "`n=== 2. Checking Dimensions & Cryptographic Uniqueness ==="
$dimMismatch = 0
$corruptCount = 0
$hashes = @{}

foreach ($f in $srcFiles) {
    try {
        $img = [System.Drawing.Image]::FromFile($f.FullName)
        if ($img.Width -ne 500 -or $img.Height -ne 600) {
            $dimMismatch++
            Write-Output "Dimension mismatch in $($f.Name): $($img.Width)x$($img.Height)"
        }
        $img.Dispose()
    }
    catch {
        $corruptCount++
        Write-Output "Failed to open $($f.Name): $($_.Exception.Message)"
    }

    $hash = (Get-FileHash -Path $f.FullName -Algorithm SHA256).Hash
    $hashes[$hash] = $f.Name
}

Write-Output "Dimension Mismatches: $dimMismatch"
Write-Output "Corrupted Files: $corruptCount"
Write-Output "Unique Hashes Count: $($hashes.Count) / $($srcFiles.Count)"

Write-Output "`n=== 3. Pixel Forensics on Palettes & Silhouettes ==="
# Test Shirt QA001 (Áo sơ mi, Trắng)
$bmp001 = New-Object System.Drawing.Bitmap (Join-Path $srcDir "QA001.jpg")
$hdr001 = $bmp001.GetPixel(250, 30)
$torso001 = $bmp001.GetPixel(250, 300)
$bg001 = $bmp001.GetPixel(30, 300)
Write-Output ("QA001 (Shirt, White): Header=(R:{0},G:{1},B:{2}), Torso=(R:{3},G:{4},B:{5}), BG=(R:{6},G:{7},B:{8})" -f `
    $hdr001.R, $hdr001.G, $hdr001.B, $torso001.R, $torso001.G, $torso001.B, $bg001.R, $bg001.G, $bg001.B)
$bmp001.Dispose()

# Test Pants QA026 (Quần Jeans, Đen)
$bmp026 = New-Object System.Drawing.Bitmap (Join-Path $srcDir "QA026.jpg")
$leftLeg026 = $bmp026.GetPixel(200, 300)
$rightLeg026 = $bmp026.GetPixel(300, 300)
$crotch026 = $bmp026.GetPixel(250, 300)
Write-Output ("QA026 (Pants, Black): LeftLeg=(R:{0},G:{1},B:{2}), RightLeg=(R:{3},G:{4},B:{5}), CrotchGap=(R:{6},G:{7},B:{8})" -f `
    $leftLeg026.R, $leftLeg026.G, $leftLeg026.B, $rightLeg026.R, $rightLeg026.G, $rightLeg026.B, $crotch026.R, $crotch026.G, $crotch026.B)
$bmp026.Dispose()

# Test Dress QA051 (Váy, Xanh Navy)
$bmp051 = New-Object System.Drawing.Bitmap (Join-Path $srcDir "QA051.jpg")
$flare051 = $bmp051.GetPixel(360, 360)
$bg051 = $bmp051.GetPixel(450, 360)
Write-Output ("QA051 (Dress, Navy): FlaredSkirt=(R:{0},G:{1},B:{2}), OutsideBG=(R:{3},G:{4},B:{5})" -f `
    $flare051.R, $flare051.G, $flare051.B, $bg051.R, $bg051.G, $bg051.B)
$bmp051.Dispose()
