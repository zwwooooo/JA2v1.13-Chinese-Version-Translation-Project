<h1>生成需要的xml内容</h1>
<pre style="border-left:10px solid #666;margin:0;padding:0;background:#333;color:#fff;height:800px;font-size:12px;overflow:auto;">
<?php
$i = 91;
while ( $i <= 127) { ?>
	&lt;ITEM>
		&lt;uiIndex><?php echo $i; ?>&lt;/uiIndex>
		&lt;szItemName>Nada&lt;/szItemName>
		&lt;szLongItemName>看到这玩意儿之时即是游戏崩溃之时……&lt;/szLongItemName>
		&lt;szItemDesc>看到这玩意儿之时即是游戏崩溃之时……&lt;/szItemDesc>
		&lt;szBRName>Using this slot will cause the game to crash.&lt;/szBRName>
		&lt;szBRDesc>看到这玩意儿之时即是游戏崩溃之时……&lt;/szBRDesc>
		&lt;usItemClass>1&lt;/usItemClass>
		&lt;nasLayoutClass>1&lt;/nasLayoutClass>
		&lt;ItemSize>72&lt;/ItemSize>
		&lt;usOverheatingCooldownFactor>100.0&lt;/usOverheatingCooldownFactor>
		&lt;STAND_MODIFIERS />
		&lt;CROUCH_MODIFIERS />
		&lt;PRONE_MODIFIERS />
	&lt;/ITEM><?php echo "\n";
	++$i;
} ?>
</pre>