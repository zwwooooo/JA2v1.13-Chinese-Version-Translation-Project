<?php
require dirname(__FILE__).'/config.php';
require dirname(__FILE__).'/dir-list.php';
?>
<!DOCTYPE html>
<html>
<head>
	<meta charset="utf-8">
	<meta http-equiv="X-UA-Compatible" content="IE=Edge">
	<title>XML Tag Repace Tool</title>
	<script type="text/javascript" src="jquery.js"></script>
	<script type="text/javascript" src="main.js"></script>
	<link rel="stylesheet" type="text/css" href="style.css" media="all">
</head>
<body>
<div class="top">《铁血联盟2》v1.13</div>
<div class="site-main">
	<h1 class="h1">2个XML文件之间的Tag内容替换</h1>
	<p class="desc">by: zwwooooo</p>

	<div class="form">
		<p class="red">注意：要先检查转换的 XML 文件头部是否有 <span>&lt;?xml version="1.0" encoding="utf-8"?&gt;</span> 不然会导致编码错误。</p>
		<form id="form" method="post" action="run.php" enctype="multipart/form-data" autocomplete="off">
			<h2 class="h2">
				文件：(<a href="" id="dir-mode-on">目录方式</a><a href="" id="dir-mode-off">文件方式</a>)
				<select id="file-o-dir" class="file-dir" type="text">
					<option value="">所有原文件 TableData 目录</option>
					<?php echo $dir_list; ?>
				</select>
				<select id="file-r-dir" class="file-dir" type="text">
					<option value="">所有被替换文件 TableData 目录</option>
					<?php echo $dir_list; ?>
				</select>
			</h2>
			<div>
				<ul class="file-list file-o-list">
					<li>
						原　文　件 *：
					</li>
				</ul>
				<input id="file-o" class="file file-o" name="filecn" type="file" required>
			</div>
			<div>
				<ul class="file-list file-r-list">
					<li>
						被替换文件 *：
					</li>
				</ul>
				<input id="file-r" class="file file-r" name="fileen" type="file" required>
			</div>

			<?php /*
			<p>原　文　件 *：
				<select name="file" required>
					<?php echo $old_files_list; ?>
				</select>
			</p>
			<p>被替换文件 *：
				<select name="file" required>
					<?php echo $new_files_list; ?>
				</select>
			</p>
			*/ ?>

			<hr>
			<div class="tags-wrapper">
				<div class="yuding">
					点击自动填写Tags”：（原文件选择文件后会自动匹配）
					<ol>
						<?php if ( $xml_tags_yuding ) { ?>
							<?php foreach ( $xml_tags_yuding as $value ) { ?>
								<li><?php echo $value['dir'] ? $value['dir'].'/' : ''; ?><a href="" data-itemlist="<?php echo $value['itemlist']; ?>" data-item="<?php echo $value['item']; ?>" data-id="<?php echo $value['id']; ?>" data-tags="<?php echo $value['tags']; ?>" data-child-itemlist="<?php echo isset($value['child_itemlist']) ? $value['child_itemlist'] : ''; ?>" data-child-item="<?php echo isset($value['child_item']) ? $value['child_item'] : ''; ?>" data-child-tags="<?php echo isset($value['child_tags']) ? $value['child_tags'] : ''; ?>" data-dir="<?php echo isset($value['dir']) ? $value['dir'] : ''; ?>"><?php echo $value['filename']; ?></a><?php echo isset($value['desc']) ? ' '.$value['desc'] : ''; ?></li>
							<?php } ?>
						<?php } ?>
					</ol>
				</div>
				<div class="tags-field">
					<h2 class="h2">ITEMLIST Tag 和 ITEM Tag</h2>
					<input id="itemlist" type="text" name="itemlist" value="" required> *
					<input id="item" type="text" name="item" value="" required> *
					<h2 class="h2">关联Tag：（如2个文件 uiIndex 必须对应）</h2>
					<p><input id="guanlian" name="id" type="text" value="" required> *</p>
					<h2 class="h2">需要替换的Tags：（如 szItemName）</h2>
					<div class="tags">
						<p><input name="tags[]" type="text" required> *</p>
					</div>
					<p><a id="add" href="">+ 增加Tag</a></p>
				</div>
			</div>
			<hr>
			<p class="submit"><input type="submit" value="运行"></p>
		</form>

		<div id="runing">
			<div class="status">
				<span class="loading"><img src="loading.gif" alt=""> 处理中...</span>
				<span class="done">完成！<a href="">继续 &gt;</a></span>
				<span class="error">错误！<a href="">返回 &gt;</a></span>
			</div>
			
			<div class="info"></div>
		</div>
	</div>
</div>

</body>
</html>