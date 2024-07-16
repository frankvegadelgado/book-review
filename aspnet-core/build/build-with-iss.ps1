# Doc
# Installation Tutorial https://learn.microsoft.com/en-us/aspnet/core/tutorials/publish-to-iis?view=aspnetcore-6.0&tabs=netcore-cli
# Download and Install https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-aspnetcore-6.0.32-windows-hosting-bundle-installer
# Power Shell Tools https://www.itprotoday.com/powershell/prompting-user-input-powershell

# COMMON PATHS

$buildFolder = (Get-Item -Path "./" -Verbose).FullName
$slnFolder = Join-Path $buildFolder "../"
$outputFolder = Join-Path $buildFolder "outputs"
$webHostFolder = Join-Path $slnFolder "src/BookReview.Web.Host"
$migrateFolder = Join-Path $slnFolder "src/BookReview.Migrator"
$testFolder = Join-Path $slnFolder "test/BookReview.Tests"
$publishFolder = Read-Host -Prompt 'Input your absolute physical directory path to publish'

## CLEAR ######################################################################

Remove-Item $outputFolder -Force -Recurse -ErrorAction Ignore
New-Item -Path $outputFolder -ItemType Directory

## RESTORE NUGET PACKAGES #####################################################

Set-Location $slnFolder
dotnet restore

## MIGRATION DATABASE SCRIPTS #####################################################

Set-Location $migrateFolder
dotnet build -c Release
dotnet run -c Release application "-q"
if ( $LASTEXITCODE -gt 0 )
{
   Return
}

## RUN UNIT TESTS #####################################################

Set-Location $testFolder
dotnet test -c Release
if ( $LASTEXITCODE -gt 0 )
{
   Return
}

## PUBLISH WEB HOST PROJECT ###################################################

Set-Location $webHostFolder
dotnet publish --output (Join-Path $outputFolder "Host")

## INSTALL ON IIS #######################################################

# Copy Host to IIS Publish Directory

Copy-Item -Path (Join-Path $outputFolder "Host/*") -Destination $publishFolder -recurse -Force 
Set-Location $publishFolder
