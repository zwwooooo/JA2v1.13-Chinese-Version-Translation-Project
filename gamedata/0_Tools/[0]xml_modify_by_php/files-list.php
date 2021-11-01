<?php
//获取文件目录列表,该方法返回数组
function getDir($dir) {
	$dirArray=array();
	if (false != ($handle = opendir ( $dir ))) {
		$i=0;
		while ( false !== ($file = readdir ( $handle )) ) {
			//去掉"“.”、“..”以及带“.xxx”后缀的文件
			if ($file != "." && $file != "..") {
				if (is_dir($dir.'/'.$file)){
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
//获取文件列表
function getFile($dir) {
	$fileArray=array();
	if (false != ($handle = opendir ( $dir ))) {
		$i=0;
		while ( false !== ($file = readdir ( $handle )) ) {
			//去掉"“.”、“..”
			if ($file != "." && $file != "..") {
				if (is_file($dir.'/'.$file) && stripos($file,'.xml') ) {
					$fileArray[$i]=array('dir'=>$dir.'/', 'file'=>$file);
					if($i==100){
						break;
					}
					$i++;
				}
			}
		}
		//关闭句柄
		closedir ( $handle );
	}
	return $fileArray;
}

$old_files_list = $new_files_list = '';

function get_file_list($work_dir) {
	$files_list = '';
	if ( is_dir($work_dir) ) {
		$files_list = '';
		$i=0;
		if ( $d1_files = getFile($work_dir) ) {
			foreach ($d1_files as $d1_file) {
				$files_list .= '<li>'.++$i.' <a href="" data-file-name="'.$d1_file['file'].'">'.$d1_file['dir'].$d1_file['file'].'</a></li>';
			}
		}

		if ($d1_dirs = getDir($work_dir)) {
			foreach ($d1_dirs as $key => $value1) {
				if ( $d2_files = getFile($work_dir.'/'.$value1) ) {
					foreach ($d2_files as $d2_file) {
						$files_list .= '<li>'.++$i.' <a href="" data-file-name="'.$d2_file['file'].'">'.$d2_file['dir'].$d2_file['file'].'</a></li>';
					}
				}
			}
		}
	}

	return $files_list;
}

if ( isset($_GET['file_o_dir']) && !empty($_GET['file_o_dir']) ) {
	$old_files_list = get_file_list($_GET['file_o_dir']);
}

if ( isset($_GET['file_r_dir']) && !empty($_GET['file_r_dir']) ) {
	$new_files_list = get_file_list($_GET['file_r_dir']);
}

header('Content-type: application/json');

if ($old_files_list) {
	echo json_encode('<ul>'.$old_files_list.'</ul>');
} elseif ($new_files_list) {
	echo json_encode('<ul>'.$new_files_list.'</ul>');
} else {
	echo json_encode(false);
}

exit();
?>