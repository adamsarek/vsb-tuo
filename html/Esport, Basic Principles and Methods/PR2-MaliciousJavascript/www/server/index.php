<?php
header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: *');
header('Access-Control-Allow-Headers: *');
ini_set("allow_url_fopen", true);

if(!file_exists('data.json')) { touch('data.json'); }
$fileData = json_decode(file_get_contents('data.json'), true);
if(empty($fileData)) { $fileData = array(); }

// Process data
$postData = json_decode(file_get_contents('php://input'), true);
if(!empty($postData)) {
	array_push($fileData, $postData);
	file_put_contents('data.json', json_encode($fileData));
}

// Get content
function getContent() {
	global $fileData;
	
	$keys = array();
	foreach($fileData as $k => $v) {
		foreach($v as $kk => $vv) {
			if(!isset($keys[$kk])) {
				$keys[$kk] = array();
			}
			foreach($vv as $kkk => $vvv) {
				if(!in_array($kkk, $keys[$kk])) {
					array_push($keys[$kk], $kkk);
				}
			}
		}
	}
	
	$content = '<table><thead><tr>';
	foreach($keys as $k => $v) {
		$content .= '<th colspan="'.count($v).'">'.$k.'</th>';
	}
	$content .= '</tr><tr>';
	foreach($keys as $k => $v) {
		foreach($v as $kk => $vv) {
			$content .= '<th>'.$vv.'</th>';
		}
	}
	$content .= '</tr></thead><tbody>';
	foreach($fileData as $k => $v) {
		$content .= '<tr>';
		foreach($v as $kk => $vv) {
			for($i = 0; $i < count($keys[$kk]); $i++) {
				if(isset($vv[array_values($keys[$kk])[$i]])) {
					$content .= '<td>'.$vv[array_values($keys[$kk])[$i]].'&nbsp;</td>';
				}
				else {
					$content .= '<td>&nbsp;</td>';
				}
			}
		}
		$content .= '</tr>';
	}
	$content .= '</tbody></table>';
	$content .= '<style>'.
	'*[colspan="0"], *[rowspan="0"] { display: none; }'.
	'body { font: 12px Helvetica, Arial, sans-serif; margin: 0px; }'.
	'table { border-collapse: collapse; border-spacing: 0px; }'.
	'tr { height: 12px; line-height: 12px; }'.
	'th, td { border: 0.5px solid #CCC; padding: 4px; }'.
	'th { background: #EEE; }'.
	'</style>';
	
	return $content;
}
?>
<!doctype html><html><body><?php print_r(getContent()); ?></body></html>