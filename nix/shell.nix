{ pkgs ? import <nixpkgs> {} }:

(pkgs.buildFHSEnv {
  name = "Pokelonia Development Environment";
  
  targetPkgs = pkgs: (with pkgs; [
    udev
    alsa-lib
    fontconfig
    glew

    icu.dev
    icu

    dotnetCorePackages.dotnet_9.sdk
    dotnetCorePackages.dotnet_9.runtime
  ]) ++ (with pkgs.xorg; [
    # Avalonia UI
    libX11
    libICE
    libSM
    libXi
    libXcursor
    libXext
    libXrandr
  ]);

  multiPkgs = pkgs: (with pkgs; [
    udev
    alsa-lib
  ]);

  runScript = "fish";
}).env
