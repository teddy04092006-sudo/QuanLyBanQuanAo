Add-Type -AssemblyName System.Drawing

$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$projectRoot = (Get-Item "$scriptDir\..\..\..").FullName
$imgDir = Join-Path $projectRoot "Images\QuanAo"

function Get-PixelRGB($bmp, $x, $y) {
    $c = $bmp.GetPixel($x, $y)
    return [pscustomobject]@{ R = $c.R; G = $c.G; B = $c.B; Hex = ('#{0:X2}{1:X2}{2:X2}' -f $c.R, $c.G, $c.B) }
}

Write-Output "=== FORENSIC PIXEL ANALYSIS ==="

# Check QA001 (Áo, Trắng)
$bmp001 = New-Object System.Drawing.Bitmap (Join-Path $imgDir "QA001.jpg")
$hdr001 = Get-PixelRGB $bmp001 250 30
$acc001 = Get-PixelRGB $bmp001 250 65
$torso001 = Get-PixelRGB $bmp001 250 300
$foot001 = Get-PixelRGB $bmp001 250 500
Write-Output ("QA001 (Áo, Trắng):")
Write-Output ("  Header (250,30) : " + $hdr001.Hex + " (Expected ~#1E293B)")
Write-Output ("  Accent (250,65) : " + $acc001.Hex + " (Expected ~#E11D48)")
Write-Output ("  Torso (250,300) : " + $torso001.Hex + " (Expected ~#F5F7FA [Trắng])")
Write-Output ("  Footer (250,500): " + $foot001.Hex + " (Expected ~#FFFFFF)")
$bmp001.Dispose()

# Check QA026 (Quần, Đen)
$bmp026 = New-Object System.Drawing.Bitmap (Join-Path $imgDir "QA026.jpg")
$crotch026 = Get-PixelRGB $bmp026 250 300 # Crotch gap between pants legs -> Background
$legL026 = Get-PixelRGB $bmp026 200 300   # Left pant leg -> Black
$legR026 = Get-PixelRGB $bmp026 300 300   # Right pant leg -> Black
Write-Output ("QA026 (Quần, Đen):")
Write-Output ("  Crotch gap (250,300): " + $crotch026.Hex + " (Expected ~#F8FAFC [Background])")
Write-Output ("  Left leg (200,300)  : " + $legL026.Hex + " (Expected ~#1E1E24 [Đen])")
Write-Output ("  Right leg (300,300) : " + $legR026.Hex + " (Expected ~#1E1E24 [Đen])")
$bmp026.Dispose()

# Check QA051 (Váy, Xanh Navy)
$bmp051 = New-Object System.Drawing.Bitmap (Join-Path $imgDir "QA051.jpg")
$skirtCenter051 = Get-PixelRGB $bmp051 250 300 # Center of flared skirt -> Navy
$skirtFlared051 = Get-PixelRGB $bmp051 360 360 # Wide flare of dress -> Navy
Write-Output ("QA051 (Váy, Xanh Navy):")
Write-Output ("  Skirt center (250,300): " + $skirtCenter051.Hex + " (Expected ~#1E3A8A [Navy])")
Write-Output ("  Flared skirt (360,360): " + $skirtFlared051.Hex + " (Expected ~#1E3A8A [Navy])")
$bmp051.Dispose()
