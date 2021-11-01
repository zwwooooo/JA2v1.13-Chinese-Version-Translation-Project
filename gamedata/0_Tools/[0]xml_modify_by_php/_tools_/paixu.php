<!DOCTYPE html>
<html>
<head>
	<meta charset="utf-8">
	<meta http-equiv="X-UA-Compatible" content="IE=Edge">
	<title><?php echo $title='对 xml 文件的 ITEM 的属性重新排序、替换'; ?></title>
	<script type="text/javascript" src="../jquery.js"></script>
	<script type="text/javascript" src="../main.js"></script>
	<link rel="stylesheet" type="text/css" href="../style.css" media="all">
</head>
<body>
<div class="site-main">
	<h1 class="h1"><?php echo $title; ?></h1>
	<hr class="hr">
	<p>例如：</p>
	<pre>
	&lt;ITEMLIST>
		&lt;ITEM>
			&lt;uiIndex>1395&lt;/uiIndex>
			&lt;szItemName>Mk18 CASV&lt;/szItemName>
			&lt;szLongItemName>Mk18 MOD0 CASV&lt;/szLongItemName>
		&lt;/ITEM>
		&lt;ITEM>
			&lt;uiIndex>1396&lt;/uiIndex>
			&lt;szItemName>Mk18 CASV&lt;/szItemName>
			&lt;szLongItemName>Mk18 MOD0 CASV&lt;/szLongItemName>
		&lt;/ITEM>
		......
	&lt;/ITEMLIST>
	</pre>
	<p>将 uiIndex 重新按 2350 开头递增替换</p>
	<hr class="hr">

	<div class="form">
		<form method="post" action="./" enctype="multipart/form-data" autocomplete="off">
			文件 *：
			<input id="file-o" class="file file-o" name="filecn" type="file" required>
			<hr class="hr">

			<h2 class="h2">ITEMLIST Tag 和 ITEM Tag</h2>
			<input id="itemlist" type="text" name="itemlist" value="ITEMLIST" required> *
			<input id="item" type="text" name="item" value="ITEM" required> *
			<h2 class="h2">需要修改值的 Tag</h2>
			<p><input id="guanlian" name="id" type="text" value="uiIndex" required> *</p>
			<h2 class="h2">递增初始值</h2>
			<p><input id="guanlian" name="id" type="text" value="" required> *</p>

		</form>
	</div>
	<?php
	function getNodesInfo($node){
		if ($node->hasChildNodes()){
			$subNodes = $node->childNodes;
			foreach ($subNodes as $subNode) {
				if ( ($subNode->nodeType != 3) || ( ($subNode->nodeType == 3) && (strlen(trim($subNode->wholeText))>=1) ) ) {
					echo "Node name: ".$subNode->nodeName."\n";
					echo "Node value: ".$subNode->nodeValue."\n";
				}
				getNodesInfo($subNode);      
			}
		}
	}
	?>
</div>
</body>
</html>