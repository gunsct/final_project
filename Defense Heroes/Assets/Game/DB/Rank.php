<?php
session_start();
$_SESSION['login'] = null;
$_SESSION['id'] = null; 
//get packet
  $packet = $_REQUEST['packet'];
  //packet decode
  $decode = json_decode($packet,true);
  //get type
  $pType = $decode['type'];
  
  //connect db
  $connect = mysqli_connect("localhost", "gunsct", "gunsct9@", "gunsct") or die("Error".mysql_errno($link));
  
  //pasing type
  switch($pType){
      case "signup":
        $pId = $decode['id'];
        $pPassword = $decode['password'];
        $bOn = false;
         
        $selectQuery = "SELECT id FROM gunsct.userinfo WHERE id LIKE '$pId'";
        $result = mysqli_query($connect, $selectQuery);
        
        while($r = $result->fetch_assoc()) {
            if($r['id'] == $pId){
               echo '이미 있는 아이디입니다.';
               $bOn = true;
               break;
            }
             $bOn = false;
        }
        if( $bOn == false){
            $insertQuery = "insert into gunsct.userinfo (id, password) values ('$pId', $pPassword);";
            mysqli_query($connect, $insertQuery);
            echo  $pId.'님 가입을 축하합니다.';
        }
       break;

      case "login":
        $pId = $decode['id'];
        $pPassword = $decode['password'];
        $bOn = false;
        
        $selectQuery = "SELECT * FROM gunsct.userinfo;";
        $result = mysqli_query($connect, $selectQuery);
        
        while($r = $result->fetch_assoc()) {
            if($r['id'] == $pId){
                if($r['password'] == $pPassword){
                    $_SESSION['login'] = true;
                    $_SESSION['id'] = $pId; 
                    
                    echo $r['id'].'님 환영합니다.';
                    $bOn = true;
                    break;
                }
                else{
                    echo '비밀번호가 틀렸습니다.';
                     break;
                }
            }
            $bOn = false;
        }
        if($bOn == false){
            echo '없는 아이디입니다.';
        }
      break;
      
      case "logout":
        session_unset();
        session_destroy();
      break;
      
      case "lookrank":
        $selectQuery = "SELECT * FROM gunsct.score ORDER BY score DESC LIMIT 10";
        $result = mysqli_query($connect, $selectQuery);
            
        while($r = $result->fetch_assoc()) {
            $arr1 = str_split($r['id']);
            if(count($arr1) < 12){
                for($i = 0; $i <12 - count($arr1);$i++){
                    array_push( $arr1," ");
                }
                if(count($arr1)%2 == 0){
                    array_push( $arr1," ");
                }
            }
        $idstr = implode ("",$arr1);
        echo 'ID: '.$idstr.'SCORE: '.$r['score']."\n";
        }
      break;
      
      case "updatascore"://패킷도 세션처리로 로그인 된 상태면 그냥 있는 아이디로 한다. 패킷 최소화
        $pId = $decode['id'];//$_SESSION['id'];
        $pScore = $decode['score'];
        $bOn = false;
        $bEcall = false;
        
        $selectQuery = "SELECT count(*) as cnt FROM gunsct.score";
        $cntTuple = mysqli_query($connect, $selectQuery);
        $selectQuery2 = "SELECT * FROM gunsct.score";
        $allTuple = mysqli_query($connect, $selectQuery2);
        
        while($rc = $cntTuple->fetch_assoc()){
            if($rc['cnt'] < 10){
                while($ra = $allTuple->fetch_assoc()){
                    if($ra['id'] == $pId && $ra['score'] == $pScore){
                         echo '이미 랭킹에 있으시군요';
                         $bEcall = true;
                         break;
                    }
                }
                
                if($bEcall == false){
                    $insertQuery = "insert into gunsct.score (id, score) values ('$pId', $pScore);";
                    mysqli_query($connect, $insertQuery);
                    
                    echo '10위안에 진입!';
                    $bOn = true;
                    break;
                }
            }
            
            if($rc['cnt'] >= 10){
                while($ra = $allTuple->fetch_assoc()){
                    if($ra['id'] == $pId && $ra['score'] == $pScore){
                         echo '이미 랭킹에 있으시군요';
                         $bEcall = true;
                         break;
                    }
                }
                
                if($bEcall == false){
                    $selectQuery = "SELECT MIN(score) as min FROM gunsct.score";
                    $minTuple = mysqli_query($connect, $selectQuery);
                    $min = 0;
                    
                    while($ra = $minTuple->fetch_assoc()){
                        $min = $ra['min'];
                        if($ra['min'] > $pScore){
                            echo '노력하세요';
                            break;
                        }
                        else{
                            $deleteQuery = "DELETE FROM gunsct.score WHERE score = $min";
                            mysqli_query($connect, $deleteQuery);
                            
                            $insertQuery = "insert into gunsct.score (id, score) values ('$pId', $pScore);";
                            mysqli_query($connect, $insertQuery);
                            
                            echo '10위안에 진입!';
                            $bOn = true;
                            break;
                        }
                    }
                }
            }
      }
      break;
      //$pType
      case "upload":
        $pId = $decode['id'];//$_SESSION['id'];
        $pHp = $decode['hp'];
        $pNormal = $decode['normal'];
        $pPowerball = $decode['powerball'];
        $pStome = $decode['stome'];
        $pMetear = $decode['metear'];
        $pPoint = $decode['point'];
        $bOn = false;
        
        $selectQuery = "SELECT id FROM gunsct.store WHERE id LIKE '$pId'";
        $result = mysqli_query($connect, $selectQuery);
        
        while($r = $result->fetch_assoc()) {
            if($r['id'] == $pId){
                $updateQuery = "UPDATE gunsct.store set hp = '$pHp', normal = '$pNormal', powerball = '$pPowerball', stome = '$pStome', metear = '$pMetear', point = $pPoint where id LIKE '$pId';";
                mysqli_query($connect, $updateQuery);
                $bOn = true;
            }
       }
       if($bOn == false){
            $insertQuery = "INSERT into gunsct.store (id, hp, normal, powerball, stome, metear, point) values ('$pId', '$pHp', '$pNormal', '$pPowerball', '$pStome', '$pMetear', '$pPoint');";
            mysqli_query($connect, $insertQuery);
       }
        
      break;
      
      case "download":
        $pId = $decode['id'];//$_SESSION['id'];
        
        $selectQuery = "SELECT id FROM gunsct.store WHERE id LIKE '$pId'";
        $result = mysqli_query($connect, $selectQuery);
        $bOn = false;
        
        while($r = $result->fetch_assoc()) {
            if($r['id'] == $pId){
                $selectQuery2 = "SELECT hp, normal, powerball, stome, metear, point FROM gunsct.store where id LIKE '$pId';";
                $allTuple2 = mysqli_query($connect, $selectQuery2);
                $r = $allTuple2->fetch_assoc();
                 
                $bOn = true;
                echo "download"." ".$r['hp']." ".$r['normal']." ".$r['powerball']." ".$r['stome']." ".$r['metear']." ".$r['point'];
            }
        }
        if($bOn == false){
            echo $_SESSION['id']."없음";
        }
      break;
  }
?>
