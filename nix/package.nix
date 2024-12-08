{
  lib,
  buildDotnetModule,
  dotnetCorePackages,
  xorg,
  fontconfig
}:
buildDotnetModule {
  pname = "Pokelonia";
  version = "0.4.0";

  dotnet-sdk = dotnetCorePackages.dotnet_9.sdk;
  dotnet-runtime = dotnetCorePackages.dotnet_9.runtime;

  src = lib.cleanSource ../.;
  projectFile = "Pokelonia/Pokelonia.csproj";
  nugetDeps = ./deps.nix;

  selfCoainedBuild = true;
  runtimeDeps = ([
    fontconfig
  ]) ++ ( with xorg; [
    libX11
    libICE
    libSM
    libXi
    libXcursor
    libXext
    libXrandr
  ]);
}