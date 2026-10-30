[Console]::OutputEncoding = [System.Text.Encoding]::UTF8

$projectRoot = Split-Path (Split-Path (Split-Path $PSScriptRoot -Parent) -Parent) -Parent
$exePath = Join-Path $projectRoot "bin\Debug\QuanLyBanQuanAo.exe"
$imgFile = [string](Join-Path $projectRoot "Images\QuanAo\QA001.jpg")

Write-Output "Testing File Lock Freedom on: $imgFile"
Write-Output "Loading Assembly: $exePath"

$asm = [System.Reflection.Assembly]::LoadFrom($exePath)
$funcType = $asm.GetType("QuanLyBanQuanAo.BLL.Function")

if ($null -eq $funcType) {
    Write-Error "Could not find QuanLyBanQuanAo.BLL.Function in assembly!"
    exit 1
}

$method = $funcType.GetMethod("LayAnhKhongKhoaFile", [System.Reflection.BindingFlags]"Public,Static")
if ($null -eq $method) {
    Write-Error "Could not find static method LayAnhKhongKhoaFile!"
    exit 1
}

Write-Output "Invoking Function.LayAnhKhongKhoaFile..."
$params = [object[]]@([string]$imgFile)
$liveImage = $method.Invoke($null, $params)

if ($null -eq $liveImage) {
    Write-Error "LayAnhKhongKhoaFile returned null!"
    exit 1
}

Write-Output "Live Image Size: $($liveImage.Width)x$($liveImage.Height)"

# Now attempt exclusive open with FileShare.None
$lockFree = $false
try {
    $stream = [System.IO.File]::Open($imgFile, [System.IO.FileMode]::Open, [System.IO.FileAccess]::ReadWrite, [System.IO.FileShare]::None)
    Write-Output "Successfully acquired exclusive ReadWrite handle with FileShare.None while image object is LIVE in memory!"
    $stream.Close()
    $stream.Dispose()
    $lockFree = $true
}
catch {
    Write-Output "FAILED to acquire exclusive handle: $($_.Exception.Message)"
    $lockFree = $false
}

$liveImage.Dispose()

if ($lockFree) {
    Write-Output "VERDICT: FILE LOCK FREE (PASS)"
    exit 0
} else {
    Write-Output "VERDICT: FILE LOCKED (FAIL)"
    exit 1
}
