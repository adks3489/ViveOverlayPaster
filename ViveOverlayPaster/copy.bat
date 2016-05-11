IF %1 == x64 (
  copy %2openvr\bin\win64\openvr_api.dll %3openvr_api.dll
  copy %2openvr\bin\win64\openvr_api.pdb %3openvr_api.pdb
) ELSE (
  copy %2openvr\bin\win32\openvr_api.dll %3openvr_api.dll
  copy %2openvr\bin\win32\openvr_api.pdb %3openvr_api.pdb
)
