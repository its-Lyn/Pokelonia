{
  description = "Pokelonia build!";

  inputs = {
    nixpkgs.url = "nixpkgs/nixpkgs-unstable";
  };

  outputs = { self, nixpkgs }:
    let
      systems = [ "x86_64-linux" ];
      legacy  = nixpkgs.legacyPackages;
    in
    {
      packages = nixpkgs.lib.genAttrs systems (system: {
        default = legacy.${system}.callPackage ./nix/package.nix {};
        pokelonia = self.packages.${system}.default;
      });
    };
}
