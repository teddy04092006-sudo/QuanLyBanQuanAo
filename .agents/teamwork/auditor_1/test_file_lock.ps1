Add-Type -AssemblyName System.Drawing

$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$projectRoot = (Get-Item "$scriptDir\..\..\..").FullName
$exePath = Join-Path $projectRoot "bin\Debug\QuanLyBanQuanAo.exe"
$sampleImg = Join-Path $projectRoot "Images\QuanAo\QA001.jpg"

$asm = [System.Reflection.Assembly]::LoadFrom($exePath)
$funcType = $asm.GetType("QuanLyBanQuanAo.BLL.Function")

Write-Output "Invoking LayAnhKhongKhoaFile on QA001.jpg..."
$img = [QuanLyBanQuanAo.BLL.Function]::LayAnhKhongKhoaFile($sampleImg)

if ($null -eq $img) {
    Write-Output "FAILED: Returned Image is null!"
    exit 1
}

Write-Output ("Loaded image size: " + $img.Width + "x" + $img.Height)

$locked = $false
try {
    # Attempt exclusive read/write access with FileShare.None while $img is held in memory
    $fs = [System.IO.File]::Open($sampleImg, [System.IO.FileMode]::Open, [System.IO.FileAccess]::ReadWrite, [System.IO.FileShare]::None)
    Write-Output "Successfully acquired EXCLUSIVE ReadWrite access to file with FileShare.None while image is in memory!"
    $fs.Close()
} catch {
    $locked = $true
    Write-Output ("ERROR: File is locked! Exception: " + $_.Exception.Message)
}

$img.Dispose()

if ($locked) {
    Write-Output "VERDICT: FILE LOCK DETECTED (FAIL)"
    exit 1
} else {
    Write-Output "VERDICT: NO FILE LOCK (PASS)"
}
