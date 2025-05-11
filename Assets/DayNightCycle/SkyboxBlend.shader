Shader "Custom/SkyboxBlend"
   {
    Properties{
        _Tint("TintColor",Color) = (.5, .5, .5, .5)
        _Blend("Blend", Range(0.0,1.0)) = 0.5
        _FrontTex("Front(+Z)", 2D) = "white" {}
        _BackTex("Back(-Z)",2D)="white"{}
        _LeftTex("Left(+X)",2D)="white"{}
        _RightTex("Right(-X)",2D)="white"{}
        _UpTex("Up(+Y)",2D)="white"{}
        _DownTex("Down(-Y)",2D)="white"{}
        _FrontTex2("2Front(+Z)",2D)="white"{}
        _BackTex2("2Back(-Z)",2D)="white"{}
        _LeftTex2("2Left(+X)",2D)="white"{}
        _RightTex2("2Right(-X)",2D)="white"{}
        _UpTex2("2Up(+Y)",2D)="white"{}
        _DownTex2("2Down(-Y)",2D)="white"{}
    }
   
SubShader
{
  Tags{"Queue"="Background"}
  Cull Off
  Fog{Mode Off}
  Lighting Off
  Color[_Tint]
  Pass
  {
    SetTexture[_FrontTex]{combine texture}
    SetTexture[_FrontTex2]{constantColor(0,0,0,[_Blend])combine texture lerp(constant)previous}
    SetTexture[_FrontTex2]{combine previous+-primary,previous*primary}
  }
  Pass
  {
    SetTexture[_BackTex]{combine texture}
    SetTexture[_BackTex2]{constantColor(0,0,0,[_Blend])combine texture lerp(constant)previous}
    SetTexture[_BackTex2]{combine previous+-primary,previous*primary}
  }
  Pass
  {
    SetTexture[_LeftTex]{combine texture}
    SetTexture[_LeftTex2]{constantColor(0,0,0,[_Blend])combine texture lerp(constant)previous}
    SetTexture[_LeftTex2]{combine previous+-primary,previous*primary}
  }
  Pass
  {
    SetTexture[_RightTex]{combine texture}
    SetTexture[_RightTex2]{constantColor(0,0,0,[_Blend])combine texture lerp(constant)previous}
    SetTexture[_RightTex2]{combine previous+-primary,previous*primary}
  }
  Pass
  {
    SetTexture[_UpTex]{combine texture}
    SetTexture[_UpTex2]{constantColor(0,0,0,[_Blend])combine texture lerp(constant)previous}
    SetTexture[_UpTex2]{combine previous+-primary,previous*primary}
  }
  Pass
  {
    SetTexture[_DownTex]{combine texture}
    SetTexture[_DownTex2]{constantColor(0,0,0,[_Blend])combine texture lerp(constant)previous}
    SetTexture[_DownTex2]{combine previous+-primary,previous*primary}
  }
}   

Fallback"RenderFX/Skybox", 1
}
