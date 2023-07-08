fetch("http://satvikaitsolutions.com/bindalapi/api/party/GetAllParty").then((data)=>{
	return data.json();
}).then((objectData)=>{
	console.log(objectData[0].partyMasterId);
  let tableData="";
objectData.map((values)=>{
tableData+=`<tr>
             <td>${values.partyMasterId}</td>
            <td>${values.partyname}</td>
            <td>${values.mobile}</td>
            <td>${values.place}</td>
			<td>${values.gracePeriod}</td>
	</tr>`;
  });
  document.getElementById("table_body").innerHTML=tableData;
})