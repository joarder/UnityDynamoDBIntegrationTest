//
// Copyright 2014-2015 Amazon.com, 
// Inc. or its affiliates. All Rights Reserved.
// 
// Licensed under the AWS Mobile SDK For Unity 
// Sample Application License Agreement (the "License"). 
// You may not use this file except in compliance with the 
// License. A copy of the License is located 
// in the "license" file accompanying this file. This file is 
// distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, express or implied. See the License 
// for the specific language governing permissions and 
// limitations under the License.
//

using UnityEngine;
using System.Collections;
using Amazon.DynamoDBv2.DataModel;
using System.Collections.Generic;
using Amazon.DynamoDBv2;
using UnityEngine.UI;
using Amazon;

namespace AWSSDK.Examples {

	public class HighLevelTableExample : DynamoDbBaseExample {
		
        private IAmazonDynamoDB _client;
        private DynamoDBContext _context;        
        public Text resultText;
        public Button createOperation;	
        
        private DynamoDBContext Context {
            get {
                if(_context == null)
                    _context = new DynamoDBContext(_client);
                    
                return _context;
            }
        }
        
        void Awake() {
			UnityInitializer.AttachToGameObject (this.gameObject);
            createOperation.onClick.AddListener(PerformCreateOperation);
            _client = Client;
        }
        
        private void PerformCreateOperation() {
			
			SchoolClassEntity mySchoolClassEntity = new SchoolClassEntity {
				domainName = "D-011",
				schoolClass = "S-011",
				classTest = "Hello",
				dateSet = "World"
            };
            
            // Save the Item.
			Context.SaveAsync(mySchoolClassEntity,(result) => {
                if(result.Exception == null)
                    resultText.text += @"Item saved.";
            });
        }                
    }
    
	[DynamoDBTable("SchoolClass")]
	public class SchoolClassEntity {
		[DynamoDBHashKey]  // Hash key.
		public string domainName { get; set; }

		[DynamoDBRangeKey] // Sort/Range key
		public string schoolClass { get; set; }

		[DynamoDBIgnore]
		//[DynamoDBProperty]
		public string classTest { get; set; }

		[DynamoDBIgnore]
		//[DynamoDBProperty]
		public string dateSet { get; set; }
	}
}