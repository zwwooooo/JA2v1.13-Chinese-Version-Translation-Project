<?php
//获取文件目录列表,该方法返回数组
function getDir($dir) {
	$dirArray=array();
	if (false != ($handle = opendir ( $dir ))) {
		$i=0;
		while ( false !== ($file = readdir ( $handle )) ) {
			//去掉"“.”、“..”以及带“.xxx”后缀的文件
			if ($file != "." && $file != "..") {
				if (is_dir($dir.'/'.$file)) {
					$dirArray[$i]=$file;
					$i++;
				}
			}
		}
		//关闭句柄
		closedir ( $handle );
	}

	return $dirArray;
}

$dir_list = '';
if ( $root = getDir('./') ) {
	foreach ($root as $dir) {
		$depth_2_dir =  './' .$dir;
		$dir_list .= '<option value="' .$depth_2_dir. '">' .$depth_2_dir. '</option>';

		//二级子目录
		if ( $depth_2 = getDir($depth_2_dir) ) {
			foreach ($depth_2 as $d2_dir) {
				$depth_3_dir =  $depth_2_dir. '/' .$d2_dir;
				$dir_list .= '<option value="' .$depth_3_dir. '">' .$depth_3_dir. '</option>';
				//三级级子目录
				if ( $depth_3 = getDir($depth_3_dir) ) {
					foreach ($depth_3 as $d3_dir) {
						$depth_4_dir =  $depth_3_dir. '/' .$d3_dir;
						$dir_list .= '<option value="' .$depth_4_dir. '">' .$depth_4_dir. '</option>';
						//四级级子目录
						if ( $depth_4 = getDir($depth_4_dir) ) {
							foreach ($depth_4 as $d4_dir) {
								$depth_5_dir =  $depth_4_dir. '/' .$d4_dir;
								$dir_list .= '<option value="' .$depth_5_dir. '">' .$depth_5_dir. '</option>';
								//五级级子目录
								if ( $depth_5 = getDir($depth_5_dir) ) {
									foreach ($depth_5 as $d5_dir) {
										$depth_6_dir =  $depth_5_dir. '/' .$d5_dir;
										$dir_list .= '<option value="' .$depth_6_dir. '">' .$depth_6_dir. '</option>';
									}
								}
							}
						}
					}
				}
			}
		}
	}
}
?>