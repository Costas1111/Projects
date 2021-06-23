<h2> <p align="center">  <b>  Αλγόριθμοι και πολυπλοκότητα  </b> </p> </h2>

  
<h3> <p align="center"> <b> 2020-2021 </b> </p> </h3>
  
  
<h3> <p align="center"> <b> Χρωματισμός γράφων </b> </p> </h3>


<h2> <p align="center">  <b>  Κωνσταντίνος Αηδόνης </b> </p> </h2>

<h4> <p align="center"> <b> 15281 </b> </p> </h4>



<h2> <p align="center"> <b> Τμήμα Πληροφορικής και Τηλεπικοινωνίων, Πανεπιστήμιο Ιωάννινων </b> </p> </h2>

<br></br>


  
<p align="center";><b> Περίληψη </b> </p> 
  
<p align="center"> 
Στην παρούσα εργασία ακολουθεί η επίλυση στο πρόβλημα χρωματισμού γραφήματος που χαρακτηρίζεται ως NP-hard πρόβλημα συνδυαστικής βελτιστοποίησης . Με την βοήθεια τεσσάρων αλγορίθμων χρωματισμού γραφήματος και την εφαρμογή τους σε γνωστά προβλήματα θα γίνει η ανάθεση όσο λιγότερο χρωμάτων στις κορυφές ενός γραφήματος ώστε κάθε γειτονική κορυφή να χρωματίζεται με διαφορετικό χρώμα. </p> 


<p align="center"> 
Διαδικασία επίλυσης προβλήματος 
Μας παρέχονται τα δεδομένα χρονοπρογραμματισμού εξετάσεων Toronto στα οποία θα εφαρμόσουμε τους παρακάτω αλγόριθμους:
First fit : Όπως βλέπουμε στην ονομασία είναι ένας άπληστος αλγόριθμος που χρωματίζει τις κορυφές με την σειρά και αναθέτει σε κάθε μια τον μικρότερο αριθμό που δεν χρησιμοποιείται από γειτονική κορυφή. </p> 

<p align="center">
DSATUR : Είναι όμοιος με τον άπληστο (greedy)  αλγόριθμο χρωματισμού , χρωματίζοντας τις κορυφές τη μια μετά την άλλη και ελέγχει ποια από της  υπόλοιπες άχρωμες κορυφές έχει τον υψηλότερο αριθμό χρωμάτων στη γειτονιά και την χρωματίζει. </p> 
  
<p align="center"> 
RLF : Ξεκινάει χρωματίζοντας την μεγαλύτερη σε μέγεθος κορυφή όπου οι γείτονες της δεν είναι χρωματισμένοι και συνεχίζει με τους αμέσως μικρότερους σε μέγεθος. </p> 
  
<p align="center"> 
Backtracking DSATUR : Είναι παρόμοιος με τον DSATUR απλώς περιλαμβάνει έναν τελεστή για δυναμική  αναδιάταξη των κορυφών όταν επανεξετάζεται ένας κόμβος. </p> 
  





<p align="center"> <b>  Κώδικας Εφαρμογής </b>




