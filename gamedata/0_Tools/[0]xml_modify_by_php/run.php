<?php
$debug = '';

//test
/*	$debug = 1;
print_r($_FILES['filecn']);
$dom_xml_cn=new DOMDocument('1.0','utf-8');
// $dom_xml_cn->load($_FILES['filecn']['tmp_name']);
$itemlist_cn=$dom_xml_cn->getElementsByTagName($_POST['itemlist']);
$itemlist_cn = $itemlist_cn->item(0);
print_r($itemlist_cn);*/

if ( $debug ) {
	//Do something
} else {
	// print_r($_POST);
	// print_r($_FILES);

	$ID = $_POST['id'];
	$tags = $_POST['tags'];
	if ( isset($_POST['filecn_dir']) && !empty($_POST['filecn_dir']) && isset($_POST['fileen_dir']) && !empty($_POST['fileen_dir']) && isset($_POST['new_file_name']) && !empty($_POST['new_file_name']) ) {
		$filecn_dir = $_POST['filecn_dir'];
		$fileen_dir = $_POST['fileen_dir'];
		$new_file_name = $_POST['new_file_name'];
	} else {
		$filecn_dir = $_FILES['filecn']['tmp_name'];
		$fileen_dir = $_FILES['fileen']['tmp_name'];
		$new_file_name = $_FILES['fileen']['name'];
	}

	// Chinese XML
	$dom_xml_cn=new DOMDocument('1.0','utf-8');
	$dom_xml_cn->load($filecn_dir);
	$itemlist_cn=$dom_xml_cn->getElementsByTagName($_POST['itemlist']);
	$itemlist_cn = $itemlist_cn->item(0);
	// print_r($itemlist_cn);

	// English XML
	$dom_xml_en=new DOMDocument('1.0','utf-8');
	$dom_xml_en->load($fileen_dir);
	$itemlist_en=$dom_xml_en->getElementsByTagName($_POST['itemlist']);
	$itemlist_en = $itemlist_en->item(0);
	// print_r($itemlist_en);


	$items_cn = $itemlist_cn->getElementsByTagName($_POST['item']);
	$items_en = $itemlist_en->getElementsByTagName($_POST['item']);
	// print_r($items_cn);
	foreach ($items_cn as $k1 => $item) {
		// $tag = $item->getElementsByTagName($ID);
		// $tag = $tag->item(0);
		// echo $tag->nodeValue.'<br>';

		// $item_cn = $items_cn[$k1];
		// $item_cn = $item_cn->getElementsByTagName($ID);
		// $item_cn = $item_cn->item(0);
		// $ID_cn = $item_cn->nodeValue;

		// $item_en = $items_en[$k1];
		// $item_en = $item_en->getElementsByTagName($ID);
		// $item_en = $item_en->item(0);
		// $ID_en = $item_en->nodeValue;

		if ( isset( $items_cn[$k1]->getElementsByTagName($ID)->item(0)->nodeValue ) && isset( $items_en[$k1]->getElementsByTagName($ID)->item(0)->nodeValue ) ) {

			$ID_cn = htmlspecialchars($items_cn[$k1]->getElementsByTagName($ID)->item(0)->nodeValue);
			$ID_en = htmlspecialchars($items_en[$k1]->getElementsByTagName($ID)->item(0)->nodeValue);
			if ($ID_cn == $ID_en) {
				foreach ($tags as $k2 => $tag) {
					if ( isset( $items_cn[$k1]->getElementsByTagName($tag)->item(0)->nodeValue ) && !empty( $items_cn[$k1]->getElementsByTagName($tag)->item(0)->nodeValue ) ) {
						if ( isset( $items_en[$k1]->getElementsByTagName($tag)->item(0)->nodeValue ) ) {
							$items_en[$k1]->getElementsByTagName($tag)->item(0)->nodeValue = htmlspecialchars($items_cn[$k1]->getElementsByTagName($tag)->item(0)->nodeValue);
						} else {
							// $newnode = $dom_xml_en->createElement($tag);
							// $newnode->nodeValue = htmlspecialchars($items_cn[$k1]->getElementsByTagName($tag)->item(0)->nodeValue);
							// $current = $items_en[$k1]->getElementsByTagName($tag)->item(0);
							// $current->appendChild($newnode);
						}

					}
				}

				if ( isset($_POST['child_itemlist']) && !empty($_POST['child_itemlist']) ) {
					$child_cn = $items_cn[$k1]->getElementsByTagName($_POST['child_item']);
					$child_en = $items_en[$k1]->getElementsByTagName($_POST['child_item']);
					// print_r($child_cn->item(0));
					foreach ($child_cn as $k3 => $child_item) {
						foreach ($_POST['child_tags'] as $k4 => $child_tag) {
							if ( isset( $child_cn[$k3]->getElementsByTagName($child_tag)->item(0)->nodeValue ) ) {
								$child_en[$k3]->getElementsByTagName($child_tag)->item(0)->nodeValue = htmlspecialchars($child_cn[$k3]->getElementsByTagName($child_tag)->item(0)->nodeValue);
							}							
						}
					}
				}
			}

		}
	}





	// $first_item->setAttribute('test', 'ooxx');
	// print_r($first_item);

	// print $dom_xml_en->saveXML();

	$file_dir_and_name = './newFiles/'.$new_file_name;
	if ( isset($_POST['dir']) && !empty($_POST['dir']) ) {
		$file_dir_and_name = './newFiles/'.$_POST['dir'].'/'.$new_file_name;
		if ( !is_dir('./newFiles/'.$_POST['dir']) ) {
			mkdir('./newFiles/'.$_POST['dir']);
		}
	}

	header('Content-type: application/json');
	if ( $dom_xml_en->save($file_dir_and_name) ) {
		echo json_encode('done');
	} else {
		echo json_encode('Error!');
	}
	exit();
}
