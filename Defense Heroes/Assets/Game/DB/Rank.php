<?php
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
      
      case "lookrank":
        $selectQuery = "SELECT * FROM gunsct.score ORDER BY score DESC LIMIT 10";
        $result = mysqli_query($connect, $selectQuery);
            
        while($r = $result->fetch_assoc()) {
        echo 'ID: '.$r['id'].' SCORE: '.$r['score']."\n";
        }
      break;
      
      case "updatascore":
        $pId = $decode['id'];
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
  }
?>
