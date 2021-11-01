<!DOCTYPE html>
<html>
<head>
	<meta charset="utf-8">
	<meta http-equiv="X-UA-Compatible" content="IE=Edge">
	<title><?php echo $title='删除 xml 文件的节点'; ?></title>
	<script type="text/javascript" src="../jquery.js"></script>
	<script type="text/javascript" src="../main.js"></script>
	<link rel="stylesheet" type="text/css" href="../style.css" media="all">

	<style>
		#del_nodes{
			max-width: 95%;
		}
	</style>
</head>
<body>
<div class="site-main">
	<h1 class="h1"><?php echo $title; ?></h1>
	<hr class="hr">

	<div class="form">
		<form method="post" action="./del-node.php" enctype="multipart/form-data" autocomplete="off">
			文件 *：
			<input class="file file-o" name="file" type="file" required>
			<hr class="hr">

			<h2 class="h2">需要删除的 Node（节点，每个节点用英文逗号分割）</h2>
			<p><input id="del_nodes" name="del_nodes" type="text" value="usItemClass,AttachmentClass,nasAttachmentClass,nasLayoutClass,AvailableAttachmentPoint,AvailableAttachmentPoint,AvailableAttachmentPoint,AvailableAttachmentPoint,AvailableAttachmentPoint,AvailableAttachmentPoint,AvailableAttachmentPoint,AvailableAttachmentPoint,AvailableAttachmentPoint,AvailableAttachmentPoint,AttachmentPoint,AttachToPointAPCost,ubClassIndex,ItemFlag,ubCursor,bSoundType,ubGraphicType,ubGraphicNum,ubWeight,ubPerPocket,ItemSize,ItemSizeBonus,usPrice,ubCoolness,bReliability,bRepairEase,Damageable,Repairable,WaterDamages,Metal,Sinks,ShowStatus,HiddenAddon,TwoHanded,NotBuyable,Attachment,HiddenAttachment,BlockIronSight,BigGunList,SciFi,NotInEditor,DefaultUndroppable,Unaerodynamic,Electronic,Inseparable,BR_NewInventory,BR_UsedInventory,BR_ROF,PercentNoiseReduction,HideMuzzleFlash,Bipod,RangeBonus,PercentRangeBonus,ToHitBonus,BestLaserRange,AimBonus,MinRangeForAimBonus,MagSizeBonus,RateOfFireBonus,BulletSpeedBonus,BurstSizeBonus,BurstToHitBonus,AutoFireToHitBonus,APBonus,PercentBurstFireAPReduction,PercentAutofireAPReduction,PercentReadyTimeAPReduction,PercentReloadTimeAPReduction,PercentAPReduction,PercentStatusDrainReduction,DamageBonus,MeleeDamageBonus,GrenadeLauncher,Duckbill,GLGrenade,Mine,Mortar,RocketLauncher,SingleShotRocketLauncher,DiscardedLauncherItem,RocketRifle,Cannon,DefaultAttachment,DefaultAttachment,DefaultAttachment,DefaultAttachment,DefaultAttachment,DefaultAttachment,DefaultAttachment,DefaultAttachment,DefaultAttachment,DefaultAttachment,BrassKnuckles,Crowbar,BloodiedItem,Rock,CamoBonus,UrbanCamoBonus,DesertCamoBonus,SnowCamoBonus,StealthBonus,FlakJacket,LeatherJacket,Directional,RemoteTrigger,LockBomb,Flare,RobotRemoteControl,Walkman,HearingRangeBonus,VisionRangeBonus,NightVisionRangeBonus,DayVisionRangeBonus,CaveVisionRangeBonus,BrightLightVisionRangeBonus,PercentTunnelVision,FlashLightRange,ThermalOptics,GasMask,Alcohol,Hardware,Medical,DrugType,CamouflageKit,LocksmithKit,Toolkit,FirstAidKit,MedicalKit,WireCutters,Canteen,GasCan,Marbles,CanAndString,Jar,XRay,Batteries,NeedsBatteries,ContainsLiquid,MetalDetector,usSpotting,FingerPrintID,TripWireActivation,TripWire,NewInv,AttachmentSystem,ScopeMagFactor,ProjectionFactor,RecoilModifierX,RecoilModifierY,PercentRecoilModifier,PercentAccuracyModifier,spreadPattern,barrel,usOverheatingCooldownFactor,overheatTemperatureModificator,overheatCooldownModificator,overheatJamThresholdModificator,overheatDamageThresholdModificator,PoisonPercentage,FoodType,LockPickModifier,CrowbarModifier,DisarmModifier,RepairModifier,DamageChance,DirtIncreaseFactor,clothestype,usActionItemFlag,randomitem,randomitemcoolnessmodificator,ItemChoiceTimeSetting,buddyitem,SleepModifier,sBackpackWeightModifier,fAllowClimbing,antitankmine,cigarette,usPortionSize,diseaseprotectionface,diseaseprotectionhand,STAND_MODIFIERS,CROUCH_MODIFIERS,PRONE_MODIFIERS" required> *</p>
			<hr>
			<p class="submit"><input type="submit" value="运行"></p>
		</form>
	</div>

	<?php
	if ( isset($_FILES['file']) && !empty($_FILES['file']) ) {
		// print_r($_POST);
		// print_r($_FILES);
		$file = $_FILES['file']['tmp_name'];
		$new_file_name = $_FILES['file']['name'];

		$del_nodes = array();
		if ( isset($_POST['del_nodes']) && !empty($_POST['del_nodes']) ) {
			$del_nodes = explode(',', $_POST['del_nodes']);
		}

		$dom = new DOMDocument('1.0','utf-8');
		$dom->load($file);


		$jsq = 0;
		$jsq_all = 0;

		foreach ( $del_nodes as $del_node ) {
			$elementToRemove  = $del_node;
			$matchingElements = $dom->getElementsByTagName($elementToRemove);
			$totalMatches     = $matchingElements->length;

			$elementsToDelete = array();
			for ($i = 0; $i < $totalMatches; $i++){
				$elementsToDelete[] = $matchingElements->item($i);
			}

			foreach ( $elementsToDelete as $elementToDelete ) {
				$elementToDelete->parentNode->removeChild($elementToDelete);
				echo $jsq++."\n";
				$jsq_all++;
			}
			echo "<hr>";
			$jsq=0;
		}
		// echo '<pre>';
		// print_r($dom);
		// echo '</pre>';


		/*#$dom_xml = $dom->firstChild;
		// echo '<pre>';
		// print_r($dom_xml);
		// echo '</pre>';
		$jsq = 0;
		function getNodesInfo($node, $del_nodes=array()){
			global $jsq, $dom;
			if ($node->hasChildNodes() && $del_nodes){
				$subNodes = $node->childNodes;
				foreach ($subNodes as $subNode) {
					if ( in_array($subNode->nodeName, $del_nodes) ) {
						$current_node_name = $subNode->nodeName;
						$current_node = $dom->getElementsByTagName($current_node_name)->item(0);
						$dom = $dom->removeChild($current_node);
						echo $jsq++."\n";

						// echo "Node name: ".$subNode->nodeName."<br>";
						// echo "Node value: ".$subNode->nodeValue."<br>";
					}
					getNodesInfo($subNode, $del_nodes);      
				}
			}
		}
		getNodesInfo($dom_xml, $del_nodes);*/


		echo '完成！';
		if ($jsq_all > 0) {
			$file_dir_and_name = './newFiles/' . $new_file_name;
			$dom->save($file_dir_and_name);
		}
	}
	?>

</div>
</body>
</html>